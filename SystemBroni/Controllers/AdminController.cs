using Microsoft.AspNetCore.Mvc;

namespace SystemBroni.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        
        [HttpGet("Panel")]
        public IActionResult Panel()
        {
            return View();
        }
    }
}
