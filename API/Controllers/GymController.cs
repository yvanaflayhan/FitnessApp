using API.DTOs;
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
        public async Task<ActionResult<IEnumerable<GymDto>>> GetGyms()
        {
            var gyms = await _gymService.GetAllGymsAsync();
            return Ok(gyms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymDto>> GetGym(int id)
        {
            var gym = await _gymService.GetGymAsync(id);

            if (gym == null)
                return NotFound();

            return Ok(gym);
        }
    }
}
