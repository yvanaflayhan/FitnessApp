using API.DTOs;
using API.Entities;

namespace API.Services
{
    public interface IGymService
    {
        Task<PagedResultDto<Gym>> GetGymsAsync(int pageNumber, int pageSize);
        Task<Gym?> GetGymAsync(int id);
        Task <Gym> AddGymAsync(Gym gym);
        Task<Gym?> UpdateGymAsync(int id, Gym gym);
        Task<bool> DeleteGymAsync(int id);
        Task <IEnumerable<GymDto>> GetAllGymsAsync();

    }
}