using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{
    [Route("api/viproom")]
    [ApiController]
    public class VipRoomController : ControllerBase
    {
        private readonly IVipRoomService _vipRoomService;

        public VipRoomController(IVipRoomService vipRoomService)
        {
            _vipRoomService = vipRoomService;
        }

        // Создать новую VIP-комнату
        [HttpPost("create")]
        public ActionResult<VipRoom> CreateNewVipRoom(VipRoom vipRoom)
        {
            if (vipRoom == null)
                return BadRequest("Переданы некорректные данные");

            var createdVipRoom = _vipRoomService.CreateVipRoom(vipRoom);
            return CreatedAtAction(nameof(GetVipRoom), new { id = createdVipRoom.Id }, createdVipRoom);
        }

        // Получить все VIP-комнаты
        [HttpGet("get/all")]
        public ActionResult<IEnumerable<VipRoom>> GetAllVipRooms()
        {
            return Ok(_vipRoomService.GetVipRooms());
        }

        // Получить VIP-комнату по ID
        [HttpGet("get/{id:Guid}")]
        public ActionResult<VipRoom> GetVipRoom(Guid id)
        {
            var vipRoom = _vipRoomService.GetVipRoomById(id);
            if (vipRoom == null)
            {
                return NotFound($"По ID ({id}) VIP-комната не найдена");
            }

            return Ok(vipRoom);
        }

        // Обновить данные VIP-комнаты по ID
        [HttpPut("update/{id:Guid}")]
        public IActionResult UpdateVipRoom(Guid id, VipRoom updatedVipRoom)
        {
            bool updated = _vipRoomService.UpdateVipRoom(id, updatedVipRoom);
            if (!updated)
            {
                return NotFound($"По ID ({id}) VIP-комната не найдена");
            }

            return NoContent();
        }

        // Удалить VIP-комнату по ID
        [HttpDelete("delete/{id:Guid}")]
        public IActionResult DeleteVipRoom(Guid id)
        {
            bool deleted = _vipRoomService.DeleteVipRoomById(id);
            if (!deleted)
                return NotFound($"По ID ({id}) VIP-комната не найдена");
            return NoContent();
        }
    }
}
