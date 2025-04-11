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


        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(Table table)
        {
            await _tableService.Create(table);
            return RedirectToAction("GetAll");
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string term = "", int pageNumber = 1, int pageSize = 10)
        {
            var tables =await _tableService
                .GetAllTableOrByName(term, pageNumber, pageSize);


            return View(new GetAllViewModelTable()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Tables = tables,
                Term = term
            });
        }


        [HttpGet("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var table = await _tableService.GetById(id);
            
            if (table is null)
                return NotFound($"По данному Id: ({id}) нет данных"); 
            
            return View(table);
        }


        [HttpPost("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, Table updatedTable)
        {
            await _tableService.Update(id, updatedTable);
            return RedirectToAction("GetAll");
        }


        [HttpGet("Delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tableService.Delete(id);
            return RedirectToAction("GetAll");
        }
    }
}