using API.DTOs;
using API.Entities;

namespace API.Services
{
    public interface ITrainerService
    {
        Task<TrainerRequest> CreateTrainerRequestAsync(TrainerRequest request);
        Task<IEnumerable<TrainerRequest>> GetTrainerRequestsAsync();
        Task<bool> ApproveTrainerRequestAsync(int id);
        Task<bool> RejectTrainerRequestAsync(int id);
        Task<bool> HasPendingTrainerRequestAsync(int userId);
        Task<IEnumerable<Trainer>> GetTrainersAsync();
        Task<bool> SubmitTrainerRequestAsync(int userId, TrainerRequestDto requestDto);
        
        
    }
}