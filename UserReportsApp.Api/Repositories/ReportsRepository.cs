using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserReportsApp.Api.Data;
using UserReportsApp.Api.Entities;
using UserReportsApp.Api.Helpers;
using UserReportsApp.Shared.Models;

namespace UserReportsApp.Api.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly UserReportsContext _context;

        public ReportsRepository(UserReportsContext context)
        {
            _context = context;
        }

        public async Task CreateReportAsync(Report report)
        {
            await _context.Reports.AddAsync(report);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateReportAsync(Report report)
        {
            _context.Reports.Update(report);

            await _context.SaveChangesAsync();
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task<IEnumerable<Report>> GetReportsByUserIdAsync(int userId)
        {
            return await _context.Reports.Where(r => r.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteReportAsync(Report report)
        {
            _context.Reports.Remove(report);

            await _context.SaveChangesAsync();
        }

        public async Task<PagingModel<Report>> GetPaginatedReportsByUserIdAsync(int userId, int page, int pageSize)
        {
            var offset = PageHelper.GetOffset(page, pageSize);

            var reports = _context.Reports.Where(r => r.UserId == userId).AsNoTracking();

            return new PagingModel<Report>
            {
                Items = await reports.Skip(offset).Take(pageSize).ToListAsync(),
                Count = await reports.CountAsync()
            };
        }
    }

    public interface IReportsRepository
    {
        Task<IEnumerable<Report>> GetReportsByUserIdAsync(int userId);
        Task<PagingModel<Report>> GetPaginatedReportsByUserIdAsync(int userId, int page, int pageSize);
        Task<Report> GetReportByIdAsync(int id);
        Task CreateReportAsync(Report report);
        Task UpdateReportAsync(Report report);
        Task DeleteReportAsync(Report report);
    }
}
