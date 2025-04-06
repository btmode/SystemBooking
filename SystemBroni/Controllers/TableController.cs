using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{

    public class TableController : Controller
    {

        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }


        [Route("/Table/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [Route("/Table/Create")]
        [HttpPost]
        public IActionResult Create(Table table)
        {
            _tableService.CreateTable(table);
            return RedirectToAction("GetAll");
        }



        [Route("/Table/GetAll")]
        [HttpGet]
        public IActionResult GetAll(string name, int pageNumber = 1, int pageSize = 10)
        {
            List<Table> tables;

            if (!string.IsNullOrEmpty(name))
            {
                HttpContext.Session.SetString("SeacrhQuery", name);

                tables = _tableService.
                    GetTablesByName(name, pageNumber, pageSize);

                ViewBag.SearchQuery = name;
            }
            else
            {
                var sessionSearchQuery = HttpContext.Session.GetString("SeacrhQuery");

                if (!string.IsNullOrEmpty(sessionSearchQuery))
                {
                    tables = _tableService.
                        GetTablesByName(sessionSearchQuery, pageNumber, pageSize);

                    ViewBag.SearchQuery = sessionSearchQuery;
                }
                else
                {
                    tables = _tableService.GetTables(pageNumber, pageSize);
                }
            }

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(tables);
        }

        //[HttpGet("/Table/GetByNumber")]
        //public IActionResult GetByNumber(string name, int pageNumber = 1, int pageSize = 10)
        //{
        //    var tables = _tableService.GetTablesByName(name, pageNumber, pageSize);

        //    if (tables == null || !tables.Any())
        //    {
        //        ViewBag.Message = $"❌ Стол с номером ({name}) не найден.";
        //        ViewBag.SearchQuery = name;
        //        return RedirectToAction("GetAll", new { pageNumber, pageSize });
        //    }

        //    ViewBag.SearchQuery = name;
        //    ViewBag.PageNumber = pageNumber;
        //    ViewBag.PageSize = pageSize;

        //    return View("GetAll", tables);
        //}



        [Route("/Table/Update/{id:Guid}")]
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var table = _tableService.GetTableById(id);
            if (table == null)
                return NotFound("Стол не найден");

            return View(table);
        }


        [Route("/Table/Update/{id:Guid}")]
        [HttpPost]
        public IActionResult Update(Guid id, Table updatedTable)
        {
            if (updatedTable == null)
                return BadRequest("Некорректные данные");

            var updated = _tableService.UpdateTable(id, updatedTable);

            if (updated == null)
                return NotFound("Стол не найден");

            return RedirectToAction("GetAll");
        }


        [HttpGet("/Table/Delete/{id:Guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            _tableService.DeleteTableById(id);
            return RedirectToAction("GetAll");

        }
    }
}
