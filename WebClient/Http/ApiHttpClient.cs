using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flurl;
using UserReportsApp.Shared.Models;
using WebClient.Models;

namespace WebClient
{
    public class ApiEndpoints
    {
        public static readonly string Users = "api/users";
        public static readonly string Reports = "api/reports";
    }

    public class ApiHttpClient : IApiHttpClient
    {
        private readonly HttpClient _httpClient;

        public ApiHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagingModel<User>> GetUsersAsync(int page, int pageSize)
        {
            var requestUrl = ApiEndpoints.Users.SetQueryParams(new { page, pageSize });

            return await _httpClient.GetFromJsonAsync<PagingModel<User>>(requestUrl);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var requestUrl = ApiEndpoints.Users.AppendPathSegment(id);

            return await _httpClient.GetFromJsonAsync<User>(requestUrl);
        }

        public async Task<bool> InsertUserAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var message = await _httpClient.PostAsJsonAsync(ApiEndpoints.Users, user);

            return message.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var requestUrl = ApiEndpoints.Users.AppendPathSegment(user.Id);

            var message = await _httpClient.PutAsJsonAsync(requestUrl, user);

            return message.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveUserAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var requestUrl = ApiEndpoints.Users.AppendPathSegment(user.Id);

            var message = await _httpClient.DeleteAsync(requestUrl);

            return message.IsSuccessStatusCode;
        }

        #region Reports

        public async Task<PagingModel<Report>> GetUserReportsAsync(int userId)
        {
            var requestUrl = ApiEndpoints.Users.AppendPathSegment(userId)
                .AppendPathSegment("reports");

            return await _httpClient.GetFromJsonAsync<PagingModel<Report>>(requestUrl);
        }

        public async Task<EntityApiActionResult<Report>> InsertReportAsync(Report report)
        {
            if (report is null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            var message = await _httpClient.PostAsJsonAsync(ApiEndpoints.Reports, report);

            return new EntityApiActionResult<Report>
            {
                Entity = await message.Content.ReadFromJsonAsync<Report>(),
                ErrorMessage = message.ReasonPhrase,
                Success = message.IsSuccessStatusCode
            };
        }

        public async Task<bool> UpdateReportAsync(Report report)
        {
            if (report is null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            var requestUrl = ApiEndpoints.Reports.AppendPathSegment(report.Id);

            var message = await _httpClient.PutAsJsonAsync(requestUrl, report);

            return message.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveReportAsync(Report report)
        {
            if (report is null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            var requestUrl = ApiEndpoints.Reports.AppendPathSegment(report.Id);

            var message = await _httpClient.DeleteAsync(requestUrl);

            return message.IsSuccessStatusCode;
        }

        #endregion
    }

    public interface IApiHttpClient
    {
        #region Users

        Task<PagingModel<User>> GetUsersAsync(int page, int pageSize);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> InsertUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> RemoveUserAsync(User user);

        #endregion

        #region Reports

        Task<PagingModel<Report>> GetUserReportsAsync(int userId);
        Task<EntityApiActionResult<Report>> InsertReportAsync(Report report);
        Task<bool> UpdateReportAsync(Report report);
        Task<bool> RemoveReportAsync(Report report);

        #endregion
    }
}
