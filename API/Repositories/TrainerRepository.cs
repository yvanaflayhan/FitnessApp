using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TrainerRepository: ITrainerRepository
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
            return await _context.TrainerRequests
            .ToListAsync();
        }
    }
}