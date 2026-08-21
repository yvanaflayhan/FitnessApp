using API.Entities;

namespace API.Services
{
    public interface ITrainerService
    {
        Task<TrainerRequest> CreateTrainerRequestAsync(TrainerRequest request);
        Task<IEnumerable<TrainerRequest>> GetTrainerRequestsAsync();
        Task<bool> ApproveTrainerRequestAsync(int id);
        Task<bool> HasPendingTrainerRequestAsync(int userId);
    }
}