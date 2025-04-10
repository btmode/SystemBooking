using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml.Linq;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{
     [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        
        // Через ILogger подключить логгер, который будет записывать в файл. path = C://log.txt
        public UserController(IUserService userService, ILogger logger)
        {
            _userService = userService;
            logger.Log(LogLevel.Information, "User controller created");
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("Create")]
        public IActionResult Create(User user)
        {
            _userService.CreateUser(user);
            return RedirectToAction("GetAll");
        }


        [HttpGet("GetAll")]
        // Todo: remove resetSearch
        public IActionResult GetAll(string term = "", bool resetSearch = false, int pageNumber = 1, int pageSize = 10)
        {
            // logger.log(LogLevel.Information, "Начался поиск всех пользователей");
            // Todo: Term
            ViewBag.SearchQuery = term;

            var users = _userService.GetUsersByName(term, pageNumber, pageSize);

            // Todo: ViewModel
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(users);
        }


        //[HttpGet("/User/GetByName")]
        //public IActionResult GetByName(string name, int pageNumber = 1, int pageSize = 10)
        //{
        //    return RedirectToAction("GetAll", new { pageNumber, pageSize, name });
        //}


        [HttpGet("Update/{id:Guid}")]
        public IActionResult Update(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound("Пользователь не найден");

            return View(user);
        }


        [HttpPost("Update/{id:Guid}")]
        public IActionResult Update(Guid id, User updatedUser)
        {
            if (updatedUser == null)
                return BadRequest("Некорректные данные");

            var updated = _userService.UpdateUser(id, updatedUser);

            if (updated == null)
                return NotFound("Пользователь не найден");

            return RedirectToAction("GetAll");
        }


        [HttpGet("Delete/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            _userService.DeleteUserById(id);
            return RedirectToAction("GetAll");
        }
    }
}