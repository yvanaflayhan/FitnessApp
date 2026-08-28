using System.Runtime.CompilerServices;
using API.DTOs;
using API.Entities;
using API.Repositories;

namespace API.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IUserRepository _userRepository;

        public TrainerService(ITrainerRepository trainerRepository, IUserRepository userRepository)
        {
            _trainerRepository = trainerRepository;
            _userRepository = userRepository;
        }

        public async Task<TrainerRequest> CreateTrainerRequestAsync(TrainerRequest request)
        {
            return await _trainerRepository.AddTrainerRequestAsync(request);
        }

        public async Task<IEnumerable<TrainerRequest>> GetTrainerRequestsAsync()
        {
            return await _trainerRepository.GetTrainerRequestsAsync();
        }

        public async Task<bool> ApproveTrainerRequestAsync(int id)
        {
            var request = await _trainerRepository.GetTrainerRequestAsync(id);

            if (request == null)
                return false;

            if (request.Status != "Pending")
                return false;

            var user = await _userRepository.GetUserAsync(request.UserId);

            if (user == null)
                return false;

            var existingTrainer = await _trainerRepository.GetTrainerByUserIdAsync(user.Id);

            Console.WriteLine($"APPROVING REQUEST: {request.Id}");
            Console.WriteLine($"REQUEST USER ID: {request.UserId}");
            Console.WriteLine($"USER ID: {user.Id}");
            Console.WriteLine($"EXISTING TRAINER ID: {existingTrainer?.Id}");

            if (existingTrainer != null)
            {
                Console.WriteLine("USER ALREADY HAS A TRAINER RECORD!");

                request.Status = "Approved";

                await _trainerRepository.UpdateTrainerRequestAsync(request);

                return true;
            }

            Console.WriteLine("NO EXISTING TRAINER - CREATING NEW TRAINER");

            var trainer = new Trainer
            {
                UserId = user.Id,
                Specialization = request.Specialization,
                Description = request.Description,
                YearsOfExperience = request.YearsOfExperience,
                Skills = request.Skills,
                Phone = request.Phone,
                ImageUrl = request.ImageUrl,
                IsApproved = true,
                User = user,
            };

            user.Role = "Trainer";

            request.Status = "Approved";

            await _trainerRepository.AddTrainerAsync(trainer);
            await _userRepository.UpdateUserAsync(user);
            await _trainerRepository.UpdateTrainerRequestAsync(request);

            return true;
        }

        public async Task<bool> HasPendingTrainerRequestAsync(int userId)
        {
            var request = await _trainerRepository.GetPendingRequestByUserIdAsync(userId);

            return request != null;
        }

        public async Task<IEnumerable<TrainerDto>> GetTrainersAsync()
        {
            var trainers = await _trainerRepository.GetTrainersAsync();

            return trainers
                .Select(t => new TrainerDto
                {
                    Id = t.Id,
                    UserId = t.UserId,
                    FullName = string.IsNullOrWhiteSpace(t.User.FullName)
                        ? t.User.UserName
                        : t.User.FullName,
                    Specialization = t.Specialization,
                    Description = t.Description,
                    YearsOfExperience = t.YearsOfExperience,
                    Skills = t.Skills,
                    Phone = t.Phone,
                    ImageUrl = t.ImageUrl,
                    IsApproved = t.IsApproved,
                })
                .ToList();
        }

        public async Task<TrainerDto?> GetTrainerByIdAsync(int id)
        {
            var trainer = await _trainerRepository.GetTrainerByIdAsync(id);

            if (trainer == null)
                return null;

            return new TrainerDto
            {
                Id = trainer.Id,
                UserId = trainer.UserId,
                FullName = trainer.User.FullName,
                Specialization = trainer.Specialization,
                Description = trainer.Description,
                YearsOfExperience = trainer.YearsOfExperience,
                Skills = trainer.Skills,
                Phone = trainer.Phone,
                ImageUrl = trainer.ImageUrl,
                IsApproved = trainer.IsApproved,
            };
        }

        public async Task<string?> SubmitTrainerRequestAsync(
            int userId,
            TrainerRequestDto requestDto
        )
        {
            var existingRequests = await _trainerRepository.GetTrainerRequestsByUserIdAsync(userId);

            var pendingRequest = existingRequests.FirstOrDefault(r => r.Status == "Pending");
            if (pendingRequest != null)
                return "You already have a pending trainer request.";

            var approvedRequest = existingRequests.FirstOrDefault(r => r.Status == "Approved");
            if (approvedRequest != null)
                return "You are already an approved trainer.";

            var request = new TrainerRequest
            {
                UserId = userId,
                Specialization = requestDto.Specialization,
                Description = requestDto.Description,
                YearsOfExperience = requestDto.YearsOfExperience,
                Skills = requestDto.Skills,
                GymId = requestDto.GymId,
                OtherGymName = requestDto.OtherGymName,
                WorksIndependently = requestDto.WorksIndependently,
                Age = requestDto.Age,
                Gender = requestDto.Gender,
                Phone = requestDto.Phone,
                Height = requestDto.Height,
                Weight = requestDto.Weight,
                ImageUrl = requestDto.ImageUrl,
                CvUrl = requestDto.CvUrl,
                Status = "Pending",
            };

            await _trainerRepository.AddTrainerRequestAsync(request);

            return null;
        }

        public async Task<bool> RejectTrainerRequestAsync(int id)
        {
            var request = await _trainerRepository.GetTrainerRequestAsync(id);
            if (request == null)
                return false;

            if (request.Status != "Pending")
                return false;

            request.Status = "Rejected";

            await _trainerRepository.UpdateTrainerRequestAsync(request);
            return true;
        }
    }
}
