using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{
    
    public class VipRoomController : Controller
    {
        private readonly IVipRoomService _vipRoomService;

        public VipRoomController(IVipRoomService vipRoomService)
        {
            _vipRoomService = vipRoomService;
        }



        [Route("/VipRoom/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [Route("/VipRoom/Create")]
        [HttpPost]
        public IActionResult Create(VipRoom vipRoom)
        {

            _vipRoomService.CreateVipRoom(vipRoom);
            return RedirectToAction("GetAll");
        }

        // Вывести все VIP-комнаты (с пагинацией)
        [HttpGet("/VipRoom/GetAll")]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var vipRooms = _vipRoomService.GetVipRooms(pageNumber, pageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(vipRooms);
        }

        // Найти VIP-комнату по номеру
        [HttpGet("/VipRoom/GetByName")]
        public IActionResult GetByName(string name, int pageNumber = 1, int pageSize = 10)
        {
            var vipRooms = _vipRoomService.GetVipRoomsByNumber(name, pageNumber, pageSize);
            if (!vipRooms.Any())
            {
                ViewBag.Message = $"❌ VIP-комната с номером \"{name}\" не найдена.";
                return RedirectToAction("GetAll", new { pageNumber, pageSize });
            }

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View("GetAll", vipRooms);
        }


        [HttpGet("/VipRoom/Update/{id:Guid}")]
        public IActionResult Update(Guid id)
        {
            var vipRoom = _vipRoomService.GetVipRoomById(id);
            if (vipRoom == null)
            {
                return NotFound("Vip комната не найдена");
            }

            return View(vipRoom);
        }


        [HttpPost("/VipRoom/Update/{id:Guid}")]
        public IActionResult Update(Guid id, VipRoom updatedVipRoom)
        {
            if (updatedVipRoom == null)
                return BadRequest("Некорректные данные");

            bool updated = _vipRoomService.UpdateVipRoom(id, updatedVipRoom);
            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction("GetAll");
        }


        [HttpPost("/VipRoom/Delete/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            bool deleted = _vipRoomService.DeleteVipRoomById(id);
            if (!deleted)
            {
                return NotFound($"По ID ({id}) VIP-комната не найдена");
            }

            return RedirectToAction("GetAll");
        }


    }
}
