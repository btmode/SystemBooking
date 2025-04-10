using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{
    [Route("VipRoomBooking")]
    public class VipRoomBookingController : Controller
    {
        private readonly IVipRoomBookingService _vipRoomBookingService;

        public VipRoomBookingController(IVipRoomBookingService vipRoomBookingService)
        {
            _vipRoomBookingService = vipRoomBookingService;
        }


        // [HttpPost("Create")]
        // public ActionResult<VipRoomBooking> CreateVipRoomBooking( VipRoomBooking booking)
        // {
        //     if (booking == null)
        //     {
        //         return BadRequest("Данные были не корректно введены");
        //     }
        //
        //     var createdBooking = _vipRoomBookingService.CreateBooking(booking);
        //
        //     return CreatedAtAction(nameof(GetByIdVipRoomBooking), new { id = createdBooking.Id }, createdBooking);
        // }


          
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<VipRoomBooking>> GetAllVipRoomBooking()
        {
            return Ok(_vipRoomBookingService.GetAllBooking());
        }


        
        // [HttpGet("get/{id:Guid}")]
        // public ActionResult<VipRoomBooking> GetByIdVipRoomBooking(Guid id)
        // {
        //     var booking = _vipRoomBookingService.GetByIdBooking(id);
        //
        //     if (booking == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return Ok(booking);
        // }


        [HttpPatch("Update/{id:Guid}")]
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


        [HttpDelete("Delete/{id:Guid}")]
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
