using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;
using SystemBroni.Views;

namespace SystemBroni.Controllers
{
    [Route("TableBooking")]
    public class TableBookingController : Controller
    {
        private readonly ITableBookingService _tableBookingService;
        private readonly ILogger<TableBookingController> _logger;

        public TableBookingController(ITableBookingService tableBookingService, ILogger<TableBookingController> logger)
        {
            _tableBookingService = tableBookingService;
            _logger = logger;
        }

        // не могу решить проблему с передачей столов 
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Tables = await _tableBookingService.GetAllTables();
            return View();
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(TableBooking booking, Guid? userId)
        {
            await _tableBookingService.Create(booking , userId); 
            return RedirectToAction("GetAll");
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string term = "", int pageNumber = 1, int pageSize = 10)
        {
            var bookings = await _tableBookingService.GetAllBookingsOrByUserName(term, pageNumber, pageSize);


            return View(new GetAllViewModelTableBooking()
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
            var booking = await _tableBookingService.GetById(id);

            if (booking is null)
                return NotFound("Бронирование не найдено");

            return View(booking);
        }


        [HttpPost("Update/{id:Guid}")]
        public async Task<IActionResult> Update(TableBooking updatedBooking)
        {
            await _tableBookingService.Update(updatedBooking);
            return RedirectToAction("GetAll");
        }


        [HttpGet("Delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tableBookingService.Delete(id);
            return RedirectToAction("GetAll");
        }
    }
}