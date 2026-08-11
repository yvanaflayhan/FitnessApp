using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        private readonly IUserService _userService;

        public AdminController (IUserService userService)
        {
            _userService = userService;
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

            if(user == null)
                return NotFound();

            return Ok(user);
            
        }
        [HttpPut("users/{id}")]
        public async Task<ActionResult<MemberDto>> UpdateUser(int id, MemberUpdateDto updateDto)
        {
            var user = await _userService.UpdateUserAsync(id, updateDto);

            if(user == null)
                return NotFound();

            return Ok(user);
            
        }
        
    }
}