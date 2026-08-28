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
        public async Task<ActionResult> SubmitTrainerRequest(
            [FromForm] TrainerRequestDto requestDto,
            IFormFile? image,
            IFormFile? cv
        )
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            // Save profile image
            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    "trainers"
                );

                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);

                await image.CopyToAsync(stream);

                requestDto.ImageUrl = $"https://localhost:5001/images/trainers/{fileName}";
            }

            // Save CV
            if (cv != null && cv.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "documents",
                    "trainers"
                );

                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(cv.FileName);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);

                await cv.CopyToAsync(stream);

                requestDto.CvUrl = $"https://localhost:5001/documents/trainers/{fileName}";
            }

            var error = await _trainerService.SubmitTrainerRequestAsync(userId, requestDto);

            if (error != null)
                return BadRequest(new { message = error });

            return Ok(new { message = "Trainer request submitted successfully." });
        }
    }
}
