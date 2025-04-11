using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IUserService
    {
        public Task<User> CreateUser(User user);
        public List<User> GetUsersByName(string name, int pageNumber, int pageSize);
        public User? GetUserById(Guid id);
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


        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public List<User> GetUsersByName(string name, int pageNumber, int pageSize)
        {
            return _context.Users
                .Where(u => u.Name.Contains(name))
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public User? GetUserById(Guid id)
        {
            return _context.Users.Find(id);
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