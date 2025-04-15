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
        private readonly ILogger<TableController> _logger;

        public TableController(ITableService tableService, ILogger<TableController> logger)
        {
            _tableService = tableService;
            _logger = logger;
            _logger.LogInformation("Table controller created");
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Table table)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(table);
                }

                await _tableService.Create(table);
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании стола");
                ModelState.AddModelError("", "Произошла ошибка при создании стола.");
                return View(table);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string term = "", int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var tables = await _tableService.GetAllTableOrByName(term, pageNumber, pageSize);

                return View(new GetAllViewModelTable
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Tables = tables,
                    Term = term
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка столов");
                ModelState.AddModelError("", "Произошла ошибка при получении списка столов.");

                return View(new GetAllViewModelTable
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Tables = new List<Table>(),
                    Term = term
                });
            }
        }

        [HttpGet("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id)
        {
            try
            {
                var table = await _tableService.GetById(id);

                if (table is null)
                {
                    ModelState.AddModelError("", $"По данному Id ({id}) нет данных");
                    return View(); // Пустая форма, но с сообщением
                }

                return View(table);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении данных стола для обновления");
                ModelState.AddModelError("", "Произошла ошибка при получении данных стола.");
                return View();
            }
        }

        [HttpPost("Update/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, Table updatedTable)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(updatedTable);
                }

                await _tableService.Update(id, updatedTable);
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении стола");
                ModelState.AddModelError("", "Произошла ошибка при обновлении стола.");
                return View(updatedTable);
            }
        }

        [HttpGet("Delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
                await _tableService.Delete(id);
                return RedirectToAction("GetAll");
        }
    }
}
