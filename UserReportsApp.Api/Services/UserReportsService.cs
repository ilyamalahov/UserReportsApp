using System;
using System.Threading.Tasks;
using AutoMapper;
using UserReportsApp.Api.Entities;
using UserReportsApp.Api.Repositories;
using UserReportsApp.Shared.Models;

namespace UserReportsApp.Api.Services
{
    public class UserReportsService : ServiceBase, IUserReportsService
    {
        private readonly IMapper _mapper;
        private readonly IReportsRepository _reportsRepository;
        private readonly IUsersRepository _usersRepository;

        public UserReportsService(
            IMapper mapper,
            IReportsRepository reportsRepository,
            IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _reportsRepository = reportsRepository;
            _usersRepository = usersRepository;
        }

        #region Reports

        public async Task<IServiceActionResult> CreateReportAsync(ReportListItemDto reportDto)
        {
            var report = _mapper.Map<Report>(reportDto);

            await _reportsRepository.CreateReportAsync(report);

            _mapper.Map(report, reportDto);

            return Success();
        }

        public async Task<IServiceActionResult> UpdateReportAsync(int id, ReportListItemDto reportDto)
        {
            var report = await _reportsRepository.GetReportByIdAsync(id);

            if (report == null)
            {
                return Error("Отчет с идентификатором {Id} не существует", id);
            }

            _mapper.Map(reportDto, report);

            await _reportsRepository.UpdateReportAsync(report);

            return Success();
        }

        public async Task<IServiceActionResult> DeleteReportAsync(int id)
        {
            var report = await _reportsRepository.GetReportByIdAsync(id);

            if (report == null)
            {
                return Error("Отчет с идентификатором {Id} не существует", id);
            }

            await _reportsRepository.DeleteReportAsync(report);

            return Success();
        }

        public async Task<PagingModel<ReportListItemDto>> GetUserReportsAsync(int userId, int page, int pageSize)
        {
            if (page < 1)
            {
                throw new ArgumentException("Параметр \"page\" должен быть больше нуля");
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Параметр \"pageSize\" должен быть больше нуля");
            }

            var reportsResult = await _reportsRepository.GetPaginatedReportsByUserIdAsync(userId, page, pageSize);

            return _mapper.Map<PagingModel<ReportListItemDto>>(reportsResult);
        }

        #endregion

        public async Task CreateUserAsync(UserListItemDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _usersRepository.CreateUserAsync(user);

            _mapper.Map(user, userDto);
        }

        public async Task UpdateUserAsync(int id, UserListItemDto userDto)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);

            if(user == null)
            {

            }

            _mapper.Map(userDto, user);

            await _usersRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);

            if(user == null)
            {

            }

            await _usersRepository.DeleteUserAsync(user);
        }

        public async Task<PagingModel<UserListItemDto>> GetUsersAsync(int page, int pageSize)
        {
            var usersResult = await _usersRepository.GetPaginatedUsersAsync(page, pageSize);

            return _mapper.Map<PagingModel<UserListItemDto>>(usersResult);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _usersRepository.GetUserByIdAsync(id);
        }
    }
}
