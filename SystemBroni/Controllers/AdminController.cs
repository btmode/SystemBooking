using Microsoft.AspNetCore.Mvc;

namespace SystemBroni.Controllers
{
    public class AdminController : Controller
    {
        [Route("/Admin/Panel")]
        [HttpGet]
        public IActionResult Panel()
        {
            return View();
        }
    }
}
