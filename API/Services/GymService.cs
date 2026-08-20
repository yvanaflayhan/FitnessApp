using API.DTOs;
using API.Entities;
using API.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.Services
{
    public class GymService: IGymService
    {
        private readonly IGymRepository _gymRepository;
        
        public GymService(IGymRepository gymRepository)
        {
            _gymRepository = gymRepository;
        }

        public async Task<PagedResultDto<Gym>> GetGymsAsync(int pageNumber, int pageSize)
        {
            return await _gymRepository.GetGymsAsync(pageNumber, pageSize);
        }

        public async Task<Gym?> GetGymAsync(int id)
        {
            return await _gymRepository.GetGymAsync(id);
        }

        public async Task<Gym> AddGymAsync(Gym gym)
        {
            return await _gymRepository.AddGymAsync(gym);
        }

        public async Task<Gym?> UpdateGymAsync(int id, Gym gym)
        {
            return await _gymRepository.UpdateGymAsync(id, gym);
        }
        public async Task<bool> DeleteGymAsync(int id)
        {
            return await _gymRepository.DeleteGymAsync(id);
        }
        
        
    }
}