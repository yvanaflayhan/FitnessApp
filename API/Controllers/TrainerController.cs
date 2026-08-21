using System.Security.Claims;
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

        [HttpPost("request")]
        public async Task<ActionResult<TrainerRequest>> CreateTrainerRequest(TrainerRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var hasPendingRequest = await _trainerService.HasPendingTrainerRequestAsync(userId);

            if (hasPendingRequest)
                return BadRequest("You already have a pending trainer request.");
            request.UserId = userId;
            request.Status = "Pending";

            var createdRequest = await _trainerService.CreateTrainerRequestAsync(request);

            return Ok(createdRequest);
        }
    }
}
