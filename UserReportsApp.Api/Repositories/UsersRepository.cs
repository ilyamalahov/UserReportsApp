using System;
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
    public class UsersRepository : IUsersRepository
    {
        private readonly UserReportsContext _context;

        public UsersRepository(UserReportsContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<PagingModel<User>> GetPaginatedUsersAsync(int page, int pageSize)
        {
            var offset = PageHelper.GetOffset(page, pageSize);

            var users = _context.Users.AsNoTracking();

            return new PagingModel<User>
            {
                Items = await users.Skip(offset)
                    .Take(pageSize)
                    .OrderBy(u => u.Id)
                    .ToListAsync(),
                Count = await users.CountAsync()
            };
        }
    }

    public interface IUsersRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<PagingModel<User>> GetPaginatedUsersAsync(int page, int pageSize);
    }
}
