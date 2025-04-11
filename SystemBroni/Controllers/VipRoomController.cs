using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;
using SystemBroni.Views;

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


        [HttpGet("/VipRoom/GetAll")]
        public IActionResult GetAll(string term = "", int pageNumber = 1, int pageSize = 10)
        {
            var vipRooms = _vipRoomService.GetVipRoomsByName(term, pageNumber, pageSize);


            return View(new GetAllViewModelVipRoom()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                VipRooms = vipRooms,
                Term = term
            });
        }


        [HttpGet("/VipRoom/Update/{id:Guid}")]
        public IActionResult Update(Guid id)
        {
            var vipRoom = _vipRoomService.GetVipRoomById(id);

            if (vipRoom == null)
                return NotFound("Vip комната не найдена");


            return View(vipRoom);
        }


        [HttpPost("/VipRoom/Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, VipRoom updatedVipRoom)
        {
            await _vipRoomService.UpdateVipRoom(id, updatedVipRoom);
            return RedirectToAction("GetAll");
        }


        [HttpGet("/VipRoom/Delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _vipRoomService.DeleteVipRoomById(id);
            return RedirectToAction("GetAll");
        }
    }
}