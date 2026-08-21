using API.Entities;

namespace API.Repositories
{
    public interface ITrainerRepository
    {
        Task<TrainerRequest> AddTrainerRequestAsync(TrainerRequest request);
        Task<IEnumerable<TrainerRequest>> GetTrainerRequestsAsync();
    }
}