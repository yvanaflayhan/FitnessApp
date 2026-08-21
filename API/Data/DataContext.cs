using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<AppUser> Users {get; set;}
        public DbSet<Gym> Gyms {get; set;}
        public DbSet<TrainerRequest> TrainerRequests {get; set;}
        
    }
}