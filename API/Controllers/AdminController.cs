using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("You are an Admin!");
        }
        
    }
}