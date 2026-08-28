using System.Security.Claims;
using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            return await _accountService.RegisterAsync(registerDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return await _accountService.LoginAsync(loginDto);
        }
        
        [Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            return await _accountService.GetCurrentUserAsync();
        }
        [Authorize]
        [HttpGet("validate")]
        public ActionResult ValidateToken()
        {
            return Ok(new
            {
                username = User.Identity?.Name,
                userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                role = User.FindFirst(ClaimTypes.Role)?.Value
            });
        }
    }
}