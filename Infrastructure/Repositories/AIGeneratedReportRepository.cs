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
    public class AIGeneratedReportRepository : IAIGeneratedRepo rtRepository
    {
        private readonly ApplicationDbContext _context;

        public AIGeneratedReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AIGeneratedReport> GetByIdAsync(int id) =>
            await _context.AIGeneratedReports.FindAsync(id);

        public async Task<IEnumerable<AIGeneratedReport>> GetAllAsync() =>
            await _context.AIGeneratedReports.ToListAsync();

        public async Task<AIGeneratedReport> AddAsync(AIGeneratedReport report)
        {
            _context.AIGeneratedReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var report = await _context.AIGeneratedReports.FindAsync(id);
            if (report == null) return false;

            _context.AIGeneratedReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        Task<OperationResult<AIGeneratedReport>> IAIGeneratedReportRepository.AddAsync(AIGeneratedReport report)
        {
            throw new NotImplementedException();
        }

        Task<OperationResult<IEnumerable<AIGeneratedReport>>> IAIGeneratedReportRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<OperationResult<AIGeneratedReport?>> IAIGeneratedReportRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<OperationResult<bool>> IAIGeneratedReportRepository.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

}
