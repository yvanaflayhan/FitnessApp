using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly DataContext _context;

        public TrainerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<TrainerRequest> AddTrainerRequestAsync(TrainerRequest request)
        {
            _context.TrainerRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<IEnumerable<TrainerRequest>> GetTrainerRequestsAsync()
        {
            return await _context
                .TrainerRequests.Include(r => r.user)
                .Include(r => r.Gym)
                .Where(r => r.Status == "Pending")
                .ToListAsync();
        }

        public async Task<TrainerRequest?> GetTrainerRequestAsync(int id)
        {
            return await _context.TrainerRequests.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddTrainerAsync(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
        }

        public async Task<TrainerRequest?> GetPendingRequestByUserIdAsync(int userId)
        {
            return await _context.TrainerRequests.FirstOrDefaultAsync(r =>
                r.UserId == userId && r.Status == "Pending"
            );
        }

        public async Task<IEnumerable<Trainer>> GetTrainersAsync()
        {
            return await _context
                .Trainers.Include(t => t.User)
                .Where(t => t.IsApproved)
                .ToListAsync();
        }

        public async Task UpdateTrainerRequestAsync(TrainerRequest request)
        {
            _context.TrainerRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task<Trainer?> GetTrainerByUserIdAsync(int userId)
        {
            return await _context.Trainers.FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<IEnumerable<TrainerRequest>> GetTrainerRequestsByUserIdAsync(int userId)
        {
            return await _context.TrainerRequests.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<Trainer?> GetTrainerByIdAsync(int id)
        {
            return await _context
                .Trainers.Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id && t.IsApproved);
        }
    }
}
