using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using UserReportsApp.Api.Data;
using UserReportsApp.Api.Entities;
using UserReportsApp.Shared.Models;

namespace UserReportsApp.Api.Services
{
    public class UserReportsService : IUserReportsService
    {
        private readonly UserReportsContext _context;
        private readonly IMapper _mapper;

        public UserReportsService(
            UserReportsContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IServiceActionResult<ReportDto>> CreateReportAsync(ReportDto reportDto)
        {
            var report = _mapper.Map<Report>(reportDto);

            await _context.Reports.AddAsync(report);

            await _context.SaveChangesAsync();

            var addedReportDto = _mapper.Map<ReportDto>(report);

            return Success(addedReportDto);
        }

        public async Task<IServiceActionResult> UpdateReportAsync(int id, ReportDto reportDto)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return Error("Отчет не существует");
            }

            _mapper.Map(reportDto, report);

            _context.Reports.Update(report);

            await _context.SaveChangesAsync();

            return Success();
        }

        public async Task<IServiceActionResult> DeleteUserAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return Error("Отчет не существует");
            }

            _context.Reports.Remove(report);

            await _context.SaveChangesAsync();

            return Success();
        }

        public async Task<PagingModel<ReportDto>> GetUserReportsAsync(int userId)
        {
            var reports = _context.Reports.Where(r => r.UserId == userId).AsNoTracking();

            return new PagingModel<ReportDto>
            {
                Items = await reports.ProjectTo<ReportDto>(_mapper.ConfigurationProvider).ToListAsync(),
                Count = await reports.CountAsync()
            };
        }

        private IServiceActionResult<TObject> Success<TObject>(TObject obj) => new SuccessObjectResult<TObject>(obj);
        private IServiceActionResult Success() => new SuccessResult();
        private IServiceActionResult Error(string errorMessage) => new ErrorResult(errorMessage);
    }

    public interface IUserReportsService
    {
        Task<IServiceActionResult<ReportDto>> CreateReportAsync(ReportDto reportDto);
        Task<IServiceActionResult> UpdateReportAsync(int id, ReportDto reportDto);
        Task<IServiceActionResult> DeleteUserAsync(int id);
        Task<PagingModel<ReportDto>> GetUserReportsAsync(int userId);
    }
}
