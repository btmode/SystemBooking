using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Table = SystemBroni.Models.Table;
using System.Reflection.Metadata;

namespace SystemBroni.Controllers
{
    public class TableBookingController : Controller
    {
        private readonly ITableBookingService _tableBookingService;

        public TableBookingController(ITableBookingService tableBookingService)
        {
            _tableBookingService = tableBookingService;
        }


        [HttpGet("/TableBooking/Create")]
        public IActionResult Create()
        {
            ViewBag.Tables = _tableBookingService.GetAllTables();

            return View();
        }


        [HttpPost("/TableBooking/Create")]
        public IActionResult Create(TableBooking booking, Table table, User user)
        {
            _tableBookingService.Create(booking, table, user);
            return RedirectToAction("GetAll");
        }


        [HttpGet("/TableBooking/GetAll")]
        public IActionResult GetAll(string name,int pageNumber = 1, int pageSize = 10)
        {
            List<TableBooking> bookings;

           
            if (!string.IsNullOrEmpty(name))
            {
                HttpContext.Session.SetString("SearchQuery", name);
                bookings = _tableBookingService.
                    GetBookingsByUserName(name,pageNumber, pageSize);

                ViewBag.SearchQuery = name;//
            }
            else
            {
                var sessionSearchQuery = HttpContext.Session.GetString("SearchQuery");

                if (!string.IsNullOrEmpty(sessionSearchQuery))
                {
                    bookings = _tableBookingService.
                        GetBookingsByUserName(sessionSearchQuery, pageNumber, pageSize);

                    ViewBag.SearchQuery = sessionSearchQuery;
                }
                else
                {
                    bookings = _tableBookingService.GetAll(pageNumber, pageSize);
                }
            }
                

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(bookings);
        }

        //[HttpGet("/TableBooking/GetByUserName")]
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


        [HttpGet("/TableBooking/Update/{id:Guid}")]
        public IActionResult Update(Guid id)
        {
            var booking = _tableBookingService.GetById(id);

            if (booking == null)
                return NotFound("Бронирование не найдено");

            return View(booking);
        }


        [HttpPost("/TableBooking/Update/{id:Guid}")]
        public IActionResult Update(TableBooking updatedBooking)
        {
            if (updatedBooking == null)
                return BadRequest("Некорректные данные");

            var updated = _tableBookingService.UpdateBooking(updatedBooking);

            if (updated == null)
                NotFound("не найден");

            return RedirectToAction("GetAll");
        }


        [HttpGet("/TableBooking/Delete/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            _tableBookingService.Delete(id);
            return RedirectToAction("GetAll");
        }
    }
}
