using Application.Interfaces.CarIssueInterface;
using Application.Interfaces.IAppDbContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CarIssueRepository : ICarIssueRepository
    {
        private readonly IAppDbContext _context;

        public CarIssueRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<CarIssue?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.CarIssues
                .FirstOrDefaultAsync(c => c.CarIssueId == id, cancellationToken);
        }

        public async Task UpdateAsync(CarIssue carIssue, CancellationToken cancellationToken)
        {
            _context.CarIssues.Update(carIssue);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}