using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<TrainerRequest> TrainerRequests { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainerGym> TrainerGyms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Trainer>()
                .HasOne(t => t.User)
                .WithOne()
                .HasForeignKey<Trainer>(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrainerGym>().HasKey(tg => new { tg.TrainerId, tg.GymId });

            modelBuilder
                .Entity<TrainerGym>()
                .HasOne(tg => tg.Trainer)
                .WithMany(t => t.TrainerGyms)
                .HasForeignKey(tg => tg.TrainerId);

            modelBuilder
                .Entity<TrainerGym>()
                .HasOne(tg => tg.Gym)
                .WithMany(g => g.TrainerGyms)
                .HasForeignKey(tg => tg.GymId);
        }
    }
}
