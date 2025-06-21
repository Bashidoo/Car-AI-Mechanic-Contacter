using Application.Common.Results;
using Application.Interfaces.AIGeneratedReportInterface;

using Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AIGeneratedReportRepository : IAIGeneratedReportRepository
    {
        private readonly AppDbContext _db;

        public AIGeneratedReportRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<OperationResult<AIGeneratedReport>> AddAsync(AIGeneratedReport report)
        {
            throw new NotImplementedException();
        }

        public Task<AIGeneratedReport> CreateAsync(AIGeneratedReport report)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<IEnumerable<AIGeneratedReport>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<AIGeneratedReport?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
