using API.Data;
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

        public async Task<IEnumerable<Gym>> GetGymsAsync()
        {
            return await _context.Gyms.ToListAsync();
        }

        public async Task<Gym?> GetGymAsync(int id)
        {
            return await _context.Gyms.FindAsync(id);
        }
    }
}