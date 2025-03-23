using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Создать нового пользователя
        [HttpPost("create")]
        public ActionResult<User> CreateNewUser(User user)
        {
            if (user == null)
                return BadRequest("Переданы некорректные данные пользователя");

            var createdUser = _userService.CreateUser(user);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // Получить всех пользователя
        [HttpGet("get/all")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {           
            return Ok(_userService.GetUsers());
        }

        // Получить пользователя по ID
        [HttpGet("get/{id:Guid}")]
        public ActionResult<User> GetUser(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound($"По данному ID ({id}) пользователь не найден");
            return Ok(user);
        }
        

        // Обновить данные пользователя
        [HttpPut("update/{id:Guid}")]
        public IActionResult UpdateUser(Guid id, User updatedUser)
        {
            bool updated = _userService.UpdateUser(id, updatedUser);
            if (!updated)
                return NotFound($"По данному ID ({id}) никаких данных нет");
            return NoContent();
        }

        // Удалить пользователя по ID
        [HttpDelete("delete/{id:Guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            bool deleted = _userService.DeleteUserById(id);
            if (!deleted)
                 return NotFound($"По данному ID ({id}) никаких данных нет");
            return NoContent();
        }

    }
}
