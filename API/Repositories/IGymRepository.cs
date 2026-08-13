using API.Entities;

namespace API.Repositories
{
    public interface IGymRepository
    {
        Task<IEnumerable<Gym>> GetGymsAsync();
        Task<Gym?> GetGymAsync(int id);
    }

}