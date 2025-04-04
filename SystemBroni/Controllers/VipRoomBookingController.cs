using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;
using System;
using System.Collections.Generic;

namespace SystemBroni.Controllers
{
    [Route("api/viproombooking")]
    [ApiController]
    public class VipRoomBookingController : ControllerBase
    {
        private readonly IVipRoomBookingService _vipRoomBookingService;

        public VipRoomBookingController(IVipRoomBookingService vipRoomBookingService)
        {
            _vipRoomBookingService = vipRoomBookingService;
        }


        // Создать бронирование
        [HttpPost]
        [Route("create")]
        public ActionResult<VipRoomBooking> CreateVipRoomBooking( VipRoomBooking booking)
        {
            if (booking == null)
            {
                return BadRequest("Данные были не корректно введены");
            }

            var createdBooking = _vipRoomBookingService.CreateBooking(booking);

            return CreatedAtAction(nameof(GetByIdVipRoomBooking), new { id = createdBooking.Id }, createdBooking);
        }


        // Получить все бронирования VIP-комнат        
        [HttpGet]
        [Route("get/all")]
        public ActionResult<IEnumerable<VipRoomBooking>> GetAllVipRoomBooking()
        {
            return Ok(_vipRoomBookingService.GetAllBooking());
        }


        // Получить бронирование по ID
        [HttpGet]
        [Route("get/{id:Guid}")]
        public ActionResult<VipRoomBooking> GetByIdVipRoomBooking(Guid id)
        {
            var booking = _vipRoomBookingService.GetByIdBooking(id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }


        // Обновить бронирование
        [HttpPut]
        [Route("update/{id:Guid}")]
        public IActionResult UpdateVipRoomBooking(Guid id, VipRoomBooking updateBooking)
        {
            if (updateBooking == null)
            {
                return BadRequest("Invalid data.");
            }

            var updated = _vipRoomBookingService.UpdateBooking(id, updateBooking);

            if (updated == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        // Удалить бронирование
        [HttpDelete]
        [Route("delete/{id:Guid}")]
        public IActionResult DeleteVipRoomBooking(Guid id)
        {
            var deleted = _vipRoomBookingService.DeleteBooking(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
