using CarDealership.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CarDealership.Application.Interfaces.Userinterface
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(User user);
        Task SaveChangesAsync(CancellationToken cancellationToken);   // ← new
        Task<User> GetByEmailAsync(string email);
    }
}