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

        public TableBookingController(ITableBookingService tableBookingService)
        {
            _tableBookingService = tableBookingService;
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.Tables = _tableBookingService.GetAllTables();
            ViewBag.Users = _tableBookingService.GetAllUsers();
            
            return View();
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(TableBooking booking, Table table, Guid userId)
        {
            await _tableBookingService.Create(booking, table, userId);

            return RedirectToAction("GetAll");
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll(string term ="", int pageNumber = 1, int pageSize = 10)
        {
            var bookings = _tableBookingService.
                GetBookingsByUserName(term, pageNumber, pageSize);
            

            return View(new GetAllViewModelTableBooking()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Bookings = bookings,
                Term = term
            });
        }




        [HttpGet("Update/{id:Guid}")]
        public IActionResult Update(Guid id)
        {
            var booking = _tableBookingService.GetById(id);

            if (booking == null)
                return NotFound("Бронирование не найдено");

            return View(booking);
        }


        [HttpPost("Update/{id:Guid}")]
        public IActionResult Update(TableBooking updatedBooking)
        {
            var updated = _tableBookingService.UpdateBooking(updatedBooking);

            if (updated == null)
                NotFound("не найден");

            return RedirectToAction("GetAll");
        }


        [HttpGet("Delete/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            _tableBookingService.Delete(id);
            return RedirectToAction("GetAll");
        }
    }
}