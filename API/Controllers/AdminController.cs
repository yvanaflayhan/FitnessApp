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
        private readonly ITrainerService _trainerService;

        public AdminController(
            IUserService userService,
            IGymService gymService,
            ITrainerService trainerService
        )
        {
            _userService = userService;
            _gymService = gymService;
            _trainerService = trainerService;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("You are an Admin!");
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            var users = await _userService.GetUsersPagedAsync(pageNumber, pageSize);

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

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet("gyms")]
        public async Task<ActionResult<PagedResultDto<Gym>>> GetGyms(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            var gyms = await _gymService.GetGymsAsync(pageNumber, pageSize);
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
        public async Task<ActionResult<Gym>> AddGym([FromForm] Gym gym, IFormFile? image)
        {
            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    "gyms"
                );
                Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);

                gym.ImageUrl = $"https://localhost:5001/images/gyms/{fileName}";
            }
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
        public async Task<IActionResult> DeleteGym(int id)
        {
            var deletedGym = await _gymService.DeleteGymAsync(id);
            if (!deletedGym)
                return NotFound();

            return NoContent();
        }

        [HttpGet("trainer-requests")]
        public async Task<ActionResult<IEnumerable<TrainerRequest>>> GetTrainerRequests()
        {
            var requests = await _trainerService.GetTrainerRequestsAsync();
            return Ok(requests);
        }

        [HttpPut("trainer-requests/{id}/approve")]
        public async Task<IActionResult> ApproveTrainerRequest(int id)
        {
            var approved = await _trainerService.ApproveTrainerRequestAsync(id);
            if (!approved)
                return NotFound();

            return NoContent();
        }
    }
}
