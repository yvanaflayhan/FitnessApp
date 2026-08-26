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

        public async Task<IEnumerable<Trainer>> GetTrainersAsync()
        {
            return await _trainerRepository.GetTrainersAsync();
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
                GymId = requestDto.GymId,
                OtherGymName = requestDto.OtherGymName,
                WorksIndependently = requestDto.WorksIndependently,
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
