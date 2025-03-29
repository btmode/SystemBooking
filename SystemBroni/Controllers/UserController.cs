using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
using SystemBroni.Models;
using SystemBroni.Service;
using static System.Net.WebRequestMethods;

namespace SystemBroni.Controllers
{

    public interface IResult
    {
        public void Execute();
    }

    //public class Result : IResult
    //{
    //    public int Code { get; set; } = 0;

    //    public void Execute()
    //    {
    //        Http.End(Code);
    //    }
    //}

    //public class OKResult : Result
    //{
    //    public int Code { get; set; } = 200;
    //}

    //public class BadResult : Result
    //{
    //    public int Code { get; set; } = 400;
    //}

    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("/User/Index")]
        public IActionResult Index()
        {
            var users = _userService.GetUsers();
            return View(users);
        }

        [Route("/User/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("api/User/Create")]
        [HttpPost]
        public IActionResult ApiCreate(User user)
        {
            _userService.CreateUser(user);
            if (user == null) return BadRequest();
            return Ok(user);
        }

        [Route("/User/Create")]
        [HttpPost]
        public IActionResult Create(User user)
        {
            _userService.CreateUser(user);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            _userService.DeleteUserById(id);
            return RedirectToAction("Index");
        }
    }
}
