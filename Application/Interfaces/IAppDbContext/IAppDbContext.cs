using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.IAppDbContext
{
    using Domain.Models; // Viktigt!

    public interface IAppDbContext
    {
        DbSet<Car> Cars { get; set; }
        DbSet<CarIssue> CarIssues { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}