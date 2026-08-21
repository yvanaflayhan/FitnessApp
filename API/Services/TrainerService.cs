using System.Runtime.CompilerServices;
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
    }
}
