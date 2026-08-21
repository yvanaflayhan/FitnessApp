using API.Entities;

namespace API.Repositories
{
    public interface ITrainerRepository
    {
        Task<TrainerRequest> AddTrainerRequestAsync(TrainerRequest request);
        Task<IEnumerable<TrainerRequest>> GetTrainerRequestsAsync();
        Task<TrainerRequest?> GetTrainerRequestAsync(int id);
        Task AddTrainerAsync(Trainer trainer);
        Task<TrainerRequest?> GetPendingRequestByUserIdAsync(int userId);
        Task<IEnumerable<Trainer>> GetTrainersAsync();
    }
}