using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CarInterface;
using Application.Interfaces.IAppDbContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly IAppDbContext _context;

        public CarRepository(IAppDbContext context) => _context = context;

        public async Task<Car> AddAsync(Car car, CancellationToken ct = default)
        {
            await _context.Cars.AddAsync(car, ct);
            await _context.SaveChangesAsync(ct);
            return car;
        }

        public async Task<IEnumerable<Car>> GetAllAsync(CancellationToken ct = default)
            => await _context.Cars
                .AsNoTracking()
                .Include(c => c.Issues)      // Ta bort Include om du laddar issues separat
                .ToListAsync(ct);

        public async Task<Car?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _context.Cars
                .Include(c => c.Issues)
                .FirstOrDefaultAsync(c => c.CarId == id, ct);

        public async Task UpdateAsync(Car car, CancellationToken ct = default)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Car car, CancellationToken ct = default)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync(ct);
        }

        public Task<bool> ExistsAsync(int id, CancellationToken ct = default)
            => _context.Cars.AnyAsync(c => c.CarId == id, ct);
    }
}
