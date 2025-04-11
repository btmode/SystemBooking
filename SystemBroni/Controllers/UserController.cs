using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml.Linq;
using SystemBroni.Models;
using SystemBroni.Service;
using SystemBroni.Views;

namespace SystemBroni.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        // Через ILogger подключить логгер, который будет записывать в файл. path = C://log.txt
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
            _logger.LogInformation("User controller created");
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(User user)
        {
            await _userService.Create(user);
            return RedirectToAction("GetAll");
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string term = "", int pageNumber = 1, int pageSize = 10)
        {
            // logger.log(LogLevel.Information, "Начался поиск всех пользователей");

            var users = await _userService
                .GetAllUsersOrByName(term, pageNumber, pageSize);

            return View(new GetAllViewModelUser()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Users = users,
                Term = term
            });
        }

        
        [HttpGet("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return View(user);
        }


        [HttpPost("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, User updatedUser)
        {
            await _userService.Update(id, updatedUser);
            return RedirectToAction("GetAll");
        }


        [HttpGet("Delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);
            return RedirectToAction("GetAll");
        }
    }
}