using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserReportsApp.Api.Data;
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
        private readonly UserReportsContext _context;

        public UsersController(
            IUserReportsService userReportsService,
            UserReportsContext context)
        {
            _userReportsService = userReportsService;
            _context = context;
        }

        [HttpGet]
        public async Task<PagingModel<User>> GetUsersAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            var offset = (page - 1) * pageSize;

            var users = _context.Users;

            return new PagingModel<User>
            {
                Items = await users.Skip(offset).Take(pageSize).OrderBy(u => u.Id).ToListAsync(),
                Count = await users.CountAsync()
            };
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateUser", user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(int id, User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var user = new User { Id = id };

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/reports")]
        public async Task<PagingModel<ReportDto>> GetUserReportsAsync(int id)
        {
            return await _userReportsService.GetUserReportsAsync(userId: id);
        }
    }
}
