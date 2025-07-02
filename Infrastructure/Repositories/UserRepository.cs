using CarDealership.Application.Interfaces.Userinterface;
using CarDealership.Domain.Entities;
using CarDealership.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CarDealership.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarDealershipDbContext _ctx;
        public UserRepository(CarDealershipDbContext ctx) =>
            _ctx = ctx;

        public Task<bool> ExistsByEmailAsync(string email) =>
            _ctx.Users.AnyAsync(u => u.Email == email);

        public Task AddAsync(User user) =>
            _ctx.Users.AddAsync(user).AsTask();

        public Task SaveChangesAsync(CancellationToken cancellationToken) =>
            _ctx.SaveChangesAsync(cancellationToken);  // ← new

        public Task<User> GetByEmailAsync(string email) =>
            _ctx.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}