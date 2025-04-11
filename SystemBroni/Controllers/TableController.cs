using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
using SystemBroni.Models;
using SystemBroni.Service;
using SystemBroni.Views;

namespace SystemBroni.Controllers
{
    [Route("Table")]
    public class TableController : Controller
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }


        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [Route("Create")]
        [HttpPost]
        public IActionResult Create(Table table)
        {
            _tableService.CreateTable(table);
            return RedirectToAction("GetAll");
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll(string term = "", int pageNumber = 1, int pageSize = 10)
        {
            var tables = _tableService
                .GetTablesByName(term, pageNumber, pageSize);


            return View(new GetAllViewModelTable()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Tables = tables,
                Term = term
            });
        }


        [HttpGet("Update/{id:Guid}")]
        public IActionResult Update(Guid id)
        {
            var table = _tableService.GetById(id);
            if (table == null)
                return NotFound("Стол не найден");

            return View(table);
        }


        [HttpPost("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, Table updatedTable)
        {
            await _tableService.UpdateTable(id, updatedTable);
            return RedirectToAction("GetAll");
        }


        [HttpGet("Delete/{id:Guid}")]
        public async Task<IActionResult>  DeleteUser(Guid id)
        {
            await _tableService.DeleteTableById(id);
            return RedirectToAction("GetAll");
        }
    }
}