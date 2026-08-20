using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class GymRepository : IGymRepository
    {
        private readonly DataContext _context;

        public GymRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedResultDto<Gym>> GetGymsAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Gyms.CountAsync();

            var gyms = await _context.Gyms
            .Skip((pageNumber -1)* pageSize)
            .Take(pageSize)
            .ToListAsync();

            return new PagedResultDto<Gym>
            {
                Items = gyms,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Gym?> GetGymAsync(int id)
        {
            return await _context.Gyms.FindAsync(id);
        }

        public async Task<Gym> AddGymAsync(Gym gym)
        {
            _context.Gyms.Add(gym);
            await _context.SaveChangesAsync();
            return gym;
        }

        public async Task<Gym?> UpdateGymAsync(int id, Gym gym)
        {
            var existingGym = await _context.Gyms.FindAsync(id);

            if (existingGym == null)
                return null;

            existingGym.Name = gym.Name;
            existingGym.Address = gym.Address;
            existingGym.Location = gym.Location;
            existingGym.Latitude = gym.Latitude;
            existingGym.Longitude = gym.Longitude;
            existingGym.Phone = gym.Phone;
            existingGym.OpeningHours = gym.OpeningHours;
            existingGym.ClosingHours = gym.ClosingHours;
            existingGym.Description = gym.Description;
            existingGym.ImageUrl = gym.ImageUrl;
            existingGym.SocialMedia = gym.SocialMedia;

            await _context.SaveChangesAsync();
            return existingGym;
        }

        public async Task<bool> DeleteGymAsync(int id)
        {
            var gym = await _context.Gyms.FindAsync(id);

            if (gym == null)
                return false;

            _context.Gyms.Remove(gym);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
