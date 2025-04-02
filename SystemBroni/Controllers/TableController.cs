using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{
  
    public class TableController : Controller
    {

        private readonly ITableService _TableService;

        public TableController(ITableService tableService)
        {
            _TableService = tableService;
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
            _TableService.CreateTable(table);
            return RedirectToAction("GetAll"); 
        }



        [Route("/Table/GetAll")]
        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var tables = _TableService.GetTables(pageNumber, pageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(tables);
        }

        [HttpGet("/Table/GetByNumber")]
        public IActionResult GetByNumber(int number, int pageNumber = 1, int pageSize = 10)
        {
            var tables = _TableService.GetTablesByNumber(number, pageNumber, pageSize);

            if (tables == null || !tables.Any())
            {
                ViewBag.Message = $"❌ Стол с номером \"{number}\" не найден.";
                ViewBag.SearchQuery = number;
                return RedirectToAction("GetAll", new { pageNumber, pageSize });
            }

            ViewBag.SearchQuery = number.ToString();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View("GetAll", tables);
        }



        [Route("/Table/Update/{id:Guid}")]
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var table = _TableService.GetTableById(id);
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

            bool updated = _TableService.UpdateTable(id, updatedTable);

            if (!updated)
                return NotFound("Стол не найден");

            return RedirectToAction("GetAll"); 
        }

        
        [HttpGet("/Table/Delete/{id:Guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            _TableService.DeleteTableById(id);
            return RedirectToAction("GetAll");
           
        }
    }
}
