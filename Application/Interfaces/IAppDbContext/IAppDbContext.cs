using Microsoft.EntityFrameworkCore;

namespace CarDealership.Application.Interfaces.IAppDbContext
{
    using CarDealership.Domain.Models; // Viktigt!

    public interface IAppDbContext
    {
        DbSet<Car> Cars { get; set; }
        DbSet<CarIssue> CarIssues { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}