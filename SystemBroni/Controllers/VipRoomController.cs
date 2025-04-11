using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;
using SystemBroni.Views;

namespace SystemBroni.Controllers
{
    [Route("VipRoom")]
    public class VipRoomController : Controller
    {
        private readonly IVipRoomService _vipRoomService;

        public VipRoomController(IVipRoomService vipRoomService)
        {
            _vipRoomService = vipRoomService;
        }


        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(VipRoom vipRoom)
        {
            await _vipRoomService.Create(vipRoom);
            return RedirectToAction("GetAll");
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string term = "", int pageNumber = 1, int pageSize = 10)
        {
            var vipRooms = await _vipRoomService.GetVipRoomsOrByName(term, pageNumber, pageSize);

            return View(new GetAllViewModelVipRoom()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                VipRooms = vipRooms,
                Term = term
            });
        }


        [HttpGet("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var vipRoom = await _vipRoomService.GetVipRoomById(id);

            if (vipRoom is null)
                return NotFound($"По данному Id: ({id}) нет данных");

            return View(vipRoom);
        }


        [HttpPost("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, VipRoom updatedVipRoom)
        {
            await _vipRoomService.UpdateVipRoom(id, updatedVipRoom);
            return RedirectToAction("GetAll");
        }


        [HttpGet("Delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _vipRoomService.DeleteVipRoomById(id);
            return RedirectToAction("GetAll");
        }
    }
}