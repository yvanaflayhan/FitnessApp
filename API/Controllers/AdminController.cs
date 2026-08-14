using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IGymService _gymService;

        public AdminController(IUserService userService, IGymService gymService)
        {
            _userService = userService;
            _gymService = gymService;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("You are an Admin!");
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("users/{id}")]
        public async Task<ActionResult<MemberDto>> UpdateUser(int id, MemberUpdateDto updateDto)
        {
            var user = await _userService.UpdateUserAsync(id, updateDto);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("gyms")]
        public async Task<ActionResult<IEnumerable<Gym>>> GetGyms()
        {
            var gyms = await _gymService.GetGymsAsync();
            return Ok(gyms);
        }

        [HttpGet("gyms/{id}")]
        public async Task<ActionResult<Gym>> GetGym(int id)
        {
            var gyms = await _gymService.GetGymAsync(id);

            if (gyms == null)
                return NotFound();

            return Ok(gyms);
        }

        [HttpPost("gyms")]
        public async Task<ActionResult<Gym>> AddGym(Gym gym)
        {
            var createdGym = await _gymService.AddGymAsync(gym);
            return Ok(createdGym);
        }

        [HttpPut("gyms/{id}")]
        public async Task<ActionResult<Gym>> UpdateGym(int id, Gym gym)
        {
            var updatedGym = await _gymService.UpdateGymAsync(id, gym);
            if (updatedGym == null)
                return NotFound();

            return Ok(updatedGym);
        }

        [HttpDelete("gyms/{id}")]
        public async Task<ActionResult<Gym>> DeleteGym(int id)
        {
            var deletedGym = await _gymService.DeleteGymAsync(id);
            if (!deletedGym)
                return NotFound();

            return NoContent();
        }
    }
}
