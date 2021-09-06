using System.Threading.Tasks;
using UserReportsApp.Api.Entities;
using UserReportsApp.Shared.Models;

namespace UserReportsApp.Api.Services
{
    public interface IUserReportsService
    {
        #region Reports

        Task<IServiceActionResult> CreateReportAsync(ReportListItemDto reportDto);
        Task<IServiceActionResult> UpdateReportAsync(int id, ReportListItemDto reportDto);
        Task<IServiceActionResult> DeleteReportAsync(int id);
        Task<PagingModel<ReportListItemDto>> GetUserReportsAsync(int userId, int page, int pageSize);

        #endregion

        #region Users

        Task CreateUserAsync(UserListItemDto user);
        Task UpdateUserAsync(int id, UserListItemDto user);
        Task DeleteUserAsync(int id);
        Task<PagingModel<UserListItemDto>> GetUsersAsync(int page, int pageSize);
        Task<User> GetUserByIdAsync(int id);

        #endregion
    }
}
