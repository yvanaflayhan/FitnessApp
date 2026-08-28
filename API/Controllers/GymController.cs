using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class GymController : BaseApiController
    {
        private readonly IGymService _gymService;
        public GymController(IGymService gymService)
        {
            _gymService = gymService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gym>>> GetGyms()
        {
            var gyms = await _gymService.GetAllGymsAsync();
            return Ok(gyms);
        }
        
        
    }
}