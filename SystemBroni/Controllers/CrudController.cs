using SystemBroni.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemBroni.Controllers
{
    public class CrudController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrudController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Crud/Menu")]
        public IActionResult Menu()
        {
            return View();
        }

        [HttpGet]
        [Route("Crud/Users")]
        public IActionResult Users()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

       


    }
}
