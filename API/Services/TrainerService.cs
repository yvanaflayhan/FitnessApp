using System.Runtime.CompilerServices;
using API.Entities;
using API.Repositories;

namespace API.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        public TrainerService(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }
        public async Task<TrainerRequest> CreateTrainerRequestAsync(TrainerRequest request)
        {
            return await _trainerRepository.AddTrainerRequestAsync(request);
        }
        public async Task<IEnumerable<TrainerRequest>> GetTrainerRequestsAsync()
        {
            return await _trainerRepository.GetTrainerRequestsAsync();
        }
    }
}