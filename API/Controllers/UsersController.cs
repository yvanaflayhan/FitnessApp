using System.Security.Claims;
using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            var user = await _userService.GetUserAsync(userId);

            if (user == null)
                return Unauthorized();

            return Ok(user);
        }

        [HttpPut("current/location")]
        public async Task<ActionResult<MemberDto>> UpdateCurrentUserLocation(
            UpdateLocationDto updateDto
        )
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            var user = await _userService.UpdateLocationAsync(userId, updateDto);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("current/username")]
        public async Task<ActionResult<MemberDto>> UpdateCurrentUsername(
            UpdateUsernameDto updateDto
        )
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            var user = await _userService.UpdateUsernameAsync(userId, updateDto);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
