using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.CarInterface
{
    //Todo: Implement the ICarRepository interface
    public interface ICarRepository
    {
        Task<Car>                AddAsync(Car car, CancellationToken ct = default);
        Task<IEnumerable<Car>>   GetAllAsync(CancellationToken ct = default);
        Task<Car?>               GetByIdAsync(int id, CancellationToken ct = default);
        Task UpdateAsync(Car car, CancellationToken ct = default);
        Task DeleteAsync(Car car, CancellationToken ct = default);
        Task<bool>               ExistsAsync(int id, CancellationToken ct = default);
    }
}
