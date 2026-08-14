using API.Entities;

namespace API.Services
{
    public interface IGymService
    {
        Task<IEnumerable<Gym>> GetGymsAsync();
        Task<Gym?> GetGymAsync(int id);
        Task <Gym> AddGymAsync(Gym gym);
        Task<Gym?> UpdateGymAsync(int id, Gym gym);
        Task<bool> DeleteGymAsync(int id);

    }
}