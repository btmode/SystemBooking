using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IUserService
    {
        public Task<User> CreateUser(User user);
        public List<User> GetUsers(int pageNumber, int pageSize);
        public List<User> GetUsersByName(string name, int pageNumber, int pageSize);
        public User? GetUserById(Guid id);
        public Task UpdateUser(Guid id, User updateUser);     
        public Task DeleteUserById(Guid id);

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

   
        public List<User> GetUsers(int pageNumber, int pageSize)
        {
            
            return _context.Users
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

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

        
        public async Task UpdateUser(Guid id, User updatedUser)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                throw new Exception("");

            user.Name = updatedUser.Name;
            user.Phone = updatedUser.Phone;

            await _context.SaveChangesAsync();
        }

      
        public async Task DeleteUserById(Guid id)
        {
            var user =  _context.Users.Find(id);

            if (user == null)
                throw new Exception("");

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

      

    }
}
