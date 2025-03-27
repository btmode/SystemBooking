using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{

    [Route("api/tablebooking")]
    [ApiController]
    public class TableBookingController : ControllerBase
    {
        private readonly ITableBookingService _tableBookingService;

        public TableBookingController(ITableBookingService tableBookingService)
        {
            _tableBookingService = tableBookingService;
        }



        // Создать бронирование
        [HttpPost]
        [Route("create")]       
        public ActionResult<TableBooking> CreateTableBooking([FromBody] TableBooking booking)
        {
            if (booking == null)
            {
                return BadRequest("Данные введены не корректно");
            }

            var createdBooking = _tableBookingService.CreateBooking(booking);

            return CreatedAtAction(nameof(GetByIdTableBooking), new { id = createdBooking.Id }, createdBooking);
        }


        // Получить все бронирования столов
        [HttpGet]
        [Route("get/all")]
        public ActionResult<IEnumerable<TableBooking>> GetAllTableBooking()
        {
            return Ok(_tableBookingService.GetAllBooking());
        }


        // Получить бронирование по ID
        [HttpGet]
        [Route("get/{id:Guid}")]
        public ActionResult<TableBooking> GetByIdTableBooking(Guid id)
        {
            var booking = _tableBookingService.GetByIdBooking(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }        


        // Обновить бронирование
        [HttpPut]
        [Route("update/{id:Guid}")]
        public IActionResult UpdateTableBooking(Guid id, [FromBody] TableBooking updateBooking)
        {
            if (updateBooking == null) return BadRequest("Данные введены не корректно");

            var updated = _tableBookingService.UpdateBooking(id, updateBooking);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }


        // Удалить бронирование
        [HttpDelete]
        [Route("delete/{id:Guid}")]
        public IActionResult DeleteTableBooking(Guid id)
        {
            var deleted = _tableBookingService.DeleteBooking(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
