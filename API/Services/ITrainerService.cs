using API.Entities;

namespace API.Services
{
    public interface ITrainerService
    {
        Task<TrainerRequest> CreateTrainerRequestAsync(TrainerRequest request);
        Task<IEnumerable<TrainerRequest>> GetTrainerRequestsAsync();
    }
}