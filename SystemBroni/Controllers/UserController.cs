using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("/User/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [Route("/User/Create")]
        [HttpPost]
        public IActionResult Create(User user)
        {
            _userService.CreateUser(user);
            return RedirectToAction("GetAll");
        }




        [Route("/User/GetAll")]
        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var users = _userService.GetUsers(pageNumber, pageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(users);
        }



        [HttpGet("/User/GetByName")]
        public IActionResult GetByName(string name, int pageNumber = 1, int pageSize = 10)
        {
            var users = _userService.GetUserByName(name, pageNumber, pageSize);

            if (users == null || !users.Any())
            {
                ViewBag.Message = $"❌ Пользователь с именем \"{name}\" не найден.";
                return View("GetAll", _userService.GetUsers(ViewBag.PageNumber, ViewBag.PageSize)); // Возвращаем список всех
            }

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchQuery = name;

            return View("GetAll", users);
        }


        [Route("/User/Update/{id:Guid}")]
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound("Пользователь не найден");

            return View(user);
        }
                
      
        [Route("/User/Update/{id:Guid}")]
        [HttpPost]
        public IActionResult Update(Guid id, User updatedUser)
        {
            if (updatedUser == null) 
                return BadRequest("Некорректные данные");

            bool updated = _userService.UpdateUser(id, updatedUser);

            if (!updated) 
                return NotFound("Пользователь не найден");

            return RedirectToAction("GetAll"); 
        }



        [HttpGet("/User/Delete/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            _userService.DeleteUserById(id);
            return RedirectToAction("GetAll");
        }


    }
}
