using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml.Linq;
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


        [HttpGet("/User/Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("/User/Create")]
        public IActionResult Create(User user)
        {
            _userService.CreateUser(user);
            return RedirectToAction("GetAll");
        }


        [HttpGet("/User/GetAll")]
        public IActionResult GetAll(string name, int pageNumber = 1, int pageSize = 10)
        {
            List<User> users;

            if (!string.IsNullOrEmpty(name))
            {
                HttpContext.Session.SetString("SearchQuery", name);

                users = _userService.GetUsersByName(name, pageNumber, pageSize);
                ViewBag.SearchQuery = name;
            }
            else
            {
                
                var sessionSearchQuery = HttpContext.Session.GetString("SearchQuery");

                if (!string.IsNullOrEmpty(sessionSearchQuery))
                {
                    users = _userService.GetUsersByName(sessionSearchQuery, pageNumber, pageSize);
                    ViewBag.SearchQuery = sessionSearchQuery;
                }
                else
                {
                    users = _userService.GetUsers(pageNumber, pageSize);
                }
            }

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(users);
        }


        //[HttpGet("/User/GetByName")]
        //public IActionResult GetByName(string name, int pageNumber = 1, int pageSize = 10)
        //{
        //    return RedirectToAction("GetAll", new { pageNumber, pageSize, name });
        //}


        [HttpGet("/User/Update/{id:Guid}")]
        public IActionResult Update(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound("Пользователь не найден");

            return View(user);
        }


        [HttpPost("/User/Update/{id:Guid}")]
        public IActionResult Update(Guid id, User updatedUser)
        {
            if (updatedUser == null)
                return BadRequest("Некорректные данные");

            var updated = _userService.UpdateUser(id, updatedUser);

            if (updated == null)
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
