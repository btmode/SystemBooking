using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;
using SystemBroni.Views;

namespace SystemBroni.Controllers
{
    [Route("VipRoomBooking")]
    public class VipRoomBookingController : Controller
    {
        private readonly IVipRoomBookingService _vipRoomBookingService;

        public VipRoomBookingController(IVipRoomBookingService vipRoomBookingService)
        {
            _vipRoomBookingService = vipRoomBookingService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.VipRooms = _vipRoomBookingService.GetAllVipRooms();
            ViewBag.Users = _vipRoomBookingService.GetAllUsers();

            return View();
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(VipRoomBooking booking, VipRoom vipRoom, Guid userId)
        {
            await _vipRoomBookingService.Create(booking, vipRoom, userId);

            return RedirectToAction("GetAll");
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string term = "", int pageNumber = 1, int pageSize = 10)
        {
            var bookings = await _vipRoomBookingService.GetAllBookingsOrByUserName(term, pageNumber, pageSize);


            return View(new GetAllViewModelVipRoomBooking()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Bookings = bookings,
                Term = term
            });
        }


        [HttpGet("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var booking = await _vipRoomBookingService.GetById(id);

            if (booking is null)
                return NotFound("Бронирование не найдено");

            return View(booking);
        }


        [HttpPost("Update/{id:Guid}")]
        public async Task<IActionResult> Update(VipRoomBooking updatedBooking)
        {
            await _vipRoomBookingService.Update(updatedBooking);
            return RedirectToAction("GetAll");
        }


        [HttpGet("Delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _vipRoomBookingService.Delete(id);
            return RedirectToAction("GetAll");
        }
        
    }
}
