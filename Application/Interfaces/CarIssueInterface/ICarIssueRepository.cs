using Domain.Models;

namespace Application.Interfaces.CarIssueInterface
{
    public interface ICarIssueRepository
    {
        Task<List<CarIssue>> GetAllAsync(CancellationToken cancellationToken);
        Task<CarIssue?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateAsync(CarIssue carIssue, CancellationToken cancellationToken);

        Task UpdateAsync(CarIssue carIssue, CancellationToken cancellationToken);
        Task DeleteAsync(CarIssue carIssue, CancellationToken cancellationToken);

    }
}