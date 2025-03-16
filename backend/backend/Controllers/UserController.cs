using Microsoft.AspNetCore.Mvc;
using backend.Models.Dtos;
using BackEnd.Repositories.Interfaces;
using backend.Models;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userService;

        public UserController(IUserInterface userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("getuserbyid{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("getuserbyusername/{name}")]
        public async Task<ActionResult<UserDto>> GetUserByUsername(string name)
        {
            var user = await _userService.GetByUsername(name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
        {
            var user = await _userService.Post(createUserDto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, ModifyUserDto modifyUserDto)
        {
            var updatedUser = await _userService.Put(id, modifyUserDto);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<user>> DeleteUser(Guid id)
        {
            var deletedUser = await _userService.DeleteById(id);

            if (deletedUser == null)
            {
                return NotFound();
            }

            return deletedUser;
        }
    }
}