using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class TrainerController : BaseApiController
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainers()
        {
            var trainers = await _trainerService.GetTrainersAsync();

            return Ok(trainers);
        }

        [HttpPost("request")]
        public async Task<ActionResult> SubmitTrainerRequest(TrainerRequestDto requestDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            var result = await _trainerService.SubmitTrainerRequestAsync(userId, requestDto);

            if (!result)
                return BadRequest("You already have a pending trainer request.");

            return Ok(new { message = "Trainer request submitted successfully." });
        }
    }
}
