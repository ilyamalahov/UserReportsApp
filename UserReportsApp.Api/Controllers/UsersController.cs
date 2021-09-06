using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserReportsApp.Api.Entities;
using UserReportsApp.Api.Services;
using UserReportsApp.Shared.Models;

namespace UserReportsApp.Api
{
    [EnableCors("WebClient")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserReportsService _userReportsService;

        public UsersController(
            IUserReportsService userReportsService)
        {
            _userReportsService = userReportsService;
        }

        [HttpGet]
        public async Task<PagingModel<UserListItemDto>> GetUsersAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            return await _userReportsService.GetUsersAsync(page, pageSize);
        }

        [HttpGet("{id:int}")]
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userReportsService.GetUserByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync([FromBody] UserListItemDto userListItemDto)
        {
            await _userReportsService.CreateUserAsync(userListItemDto);

            return CreatedAtAction("CreateUser", userListItemDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUserAsync(int id, [FromBody] UserListItemDto userListItemDto)
        {
            await _userReportsService.UpdateUserAsync(id, userListItemDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            await _userReportsService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
