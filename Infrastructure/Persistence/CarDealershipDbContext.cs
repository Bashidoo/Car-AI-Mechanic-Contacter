using Application.Interfaces.IAppDbContext;
using CarDealership.Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Infrastructure.Persistence
{
    public class CarDealershipDbContext : DbContext, IAppDbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarIssue> CarIssues { get; set; }
        public DbSet<User> Users { get; set; }

        public CarDealershipDbContext(DbContextOptions<CarDealershipDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await base.SaveChangesAsync(cancellationToken);
    }
}