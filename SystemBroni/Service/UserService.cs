using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IUserService
    {
        public Task<User> Create(User user);
        public Task<List<User?>> GetAllUsersOrByName(string name, int pageNumber, int pageSize);
        public Task<User?> GetUserById(Guid id);
        public Task Update(Guid id, User updateUser);
        public Task Delete(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<User> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User?>> GetAllUsersOrByName(string name, int pageNumber, int pageSize)
        {
            return await _context.Users
                .Where(u => u.Name.Contains(name))
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return  await _context.Users
                .AsNoTracking()
                .FirstAsync(a=>a.Id == id);
        }


        public async Task Update(Guid id, User updatedUser)
        {
            await _context.Users
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Name, updatedUser.Name)
                    .SetProperty(u => u.Phone, updatedUser.Phone));
        }

        public async Task Delete(Guid id)
        {
            await _context.Users
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}