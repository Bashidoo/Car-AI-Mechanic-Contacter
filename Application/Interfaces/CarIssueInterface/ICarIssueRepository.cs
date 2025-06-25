using Domain.Models;

namespace Application.Interfaces.CarIssueInterface
{
    public interface ICarIssueRepository
    {
        Task<CarIssue?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(CarIssue carIssue, CancellationToken cancellationToken);
        Task DeleteAsync(CarIssue carIssue, CancellationToken cancellationToken);
    }
}