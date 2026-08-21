using API.Data;
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
            return await _context.TrainerRequests.ToListAsync();
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
    }
}
