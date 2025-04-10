using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;
using SystemBroni.Views;

namespace SystemBroni.Controllers
{
    [Route("TableBooking")]
    [ApiController]
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
        public IActionResult GetAll(string term, int pageNumber = 1, int pageSize = 10)
        {
            List<TableBooking> bookings;

            if (!string.IsNullOrEmpty(term))
            {
                HttpContext.Session.SetString("SearchQuery", term);
                bookings = _tableBookingService.GetBookingsByUserName(term, pageNumber, pageSize);

                ViewBag.SearchQuery = term;
            }
            else
            {
                var sessionSearchQuery = HttpContext.Session.GetString("SearchQuery");

                if (!string.IsNullOrEmpty(sessionSearchQuery))
                {
                    bookings = _tableBookingService.GetBookingsByUserName(sessionSearchQuery, pageNumber, pageSize);

                    ViewBag.SearchQuery = sessionSearchQuery;
                }
                else
                {
                    bookings = _tableBookingService.GetAll(pageNumber, pageSize);
                }
            }

            return View(new GetAllViewModel() { PageNumber = pageNumber, PageSize = pageSize, Bookings = bookings});
        }

        //[HttpGet("GetByUserName")]
        //public IActionResult GetByUserName(string name, int pageNumber = 1, int pageSize = 10)
        //{
        //    var bookings = _tableBookingService.GetBookingsByUserName(name, pageNumber, pageSize);

        //    if (bookings == null || !bookings.Any())
        //    {
        //        ViewBag.Message = $"❌ Бронирование для {name} не найдено.";
        //        ViewBag.SearchQuery = name;
        //        return RedirectToAction("GetAll", new { pageNumber, pageSize });
        //    }

        //    ViewBag.SearchQuery = name;
        //    ViewBag.PageNumber = pageNumber;
        //    ViewBag.PageSize = pageSize;

        //    return View("GetAll", bookings);
        //}


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
            if (updatedBooking == null)
                return BadRequest("Некорректные данные");

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