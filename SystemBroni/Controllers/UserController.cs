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
        public IActionResult GetAll()
        {
            var users = _userService.GetUsers();
            return View(users);
        }


    
        [HttpGet("/User/GetByName")]
        public ActionResult<User> GetUser(string name)
        {
            var user = _userService.GetUserByName(name);

            if (user == null)
                return NotFound($"С таким именем ({name}) пользователь не найден ");

            return RedirectToAction("GetAll");
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
            if (updatedUser == null) return BadRequest("Некорректные данные");

            bool updated = _userService.UpdateUser(id, updatedUser);

            if (!updated) return NotFound("Пользователь не найден");

            return RedirectToAction("GetAll");
        }


        
        [HttpGet("/User/Delete/{id:Guid}")] 
        public IActionResult Delete(Guid id)
        {
            _userService.DeleteUserById(id);
            return RedirectToAction("GetAll");
        }



        // Обновить данные пользователя
        [Route("/User/Update")]
        [HttpPut("update/{id:Guid}")]
        public IActionResult UpdateUser(Guid id, User updatedUser)
        {
            bool updated = _userService.UpdateUser(id, updatedUser);
            if (!updated)
                return NotFound($"По данному ID ({id}) никаких данных нет");

            return NoContent();
        }



    }
}
