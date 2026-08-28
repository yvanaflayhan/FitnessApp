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
        Task<IEnumerable<TrainerDto>> GetTrainersAsync();
        Task<string?> SubmitTrainerRequestAsync(int userId, TrainerRequestDto requestDto);
        Task<TrainerDto?> GetTrainerByIdAsync(int id);
    }
}
