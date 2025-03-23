using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{
    [Route("api/table")]
    [ApiController]
    public class TableController : ControllerBase
    {

        private readonly ITableService _TableService;

        public TableController(ITableService tableService)
        {
            _TableService = tableService;
        }

        // Создать нововый стол
        [HttpPost("create")]
        public ActionResult<Table> CreateNewTable(Table table)
        {
            if (table == null)
                return BadRequest("Переданы некорректные данные пользователя");

            var createdTable = _TableService.CreateTable(table);
            return CreatedAtAction(nameof(GetTable), new { id = createdTable.Id }, createdTable);
        }

        // Получить всех пользователя
        [HttpGet("get/all")]
        public ActionResult<IEnumerable<Table>> GetAllTables()
        {
            return Ok(_TableService.GetTables());
        }

        // Получить стол по ID
        [HttpGet("get/{id:Guid}")]
        public ActionResult<Table> GetTable(Guid id)
        {
            var table = _TableService.GetTableById(id);
            if (table == null)
            {
                return NotFound($"По данному ID ({id}) стол не найден");
            }

            return Ok(table);
        }


        // Обновить данные стола по ID
        [HttpPut("update/{id:Guid}")]
        public IActionResult UpdateTables(Guid id, Table updatedTable)
        {
            bool updated = _TableService.UpdateTable(id, updatedTable);
            if (!updated)
            {
                return NotFound($"По данному ID ({id}) никаких данных нет");
            }

            return NoContent();
        }

        // Удалить пользователя по ID
        [HttpDelete("delete/{id:Guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            bool deleted = _TableService.DeleteTableById(id);
            if (!deleted)
                return NotFound($"По данному ID ({id}) никаких данных нет");
            return NoContent();
        }
    }
}
