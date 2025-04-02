using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IUserService
    {
        public User CreateUser(User user);
        public IEnumerable<User> GetUsers(int pageNumber, int pageSize);
        public IEnumerable<User> GetUsersByName(string name, int pageNumber, int pageSize);
        public User GetUserById(Guid id);
        public bool UpdateUser(Guid id, User updateUser);
        public bool DeleteUserById(Guid id);

    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        
        public User CreateUser(User user)
        {
            _context.Users.Add(user);

            _context.SaveChanges();
            return user;
        }

       
        public IEnumerable<User> GetUsers(int pageNumber, int pageSize)
        {
            return _context.Users.OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
           
        }

        
        public IEnumerable<User> GetUsersByName(string name, int pageNumber, int pageSize)
        {
            return _context.Users
                .Where(u => u.Name == name)
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }


        public User GetUserById(Guid id)
        {
            return _context.Users.Find(id);
        }

       
        public bool UpdateUser(Guid id, User updatedUser)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return false;

            user.Name = updatedUser.Name;
            user.Phone = updatedUser.Phone;

            _context.SaveChanges();
            return true;
        }

       
        public bool DeleteUserById(Guid id)
        {
            var user = _context.Users.Find(id);

            if (user == null) 
                return false;

            _context.Users.Remove(user);

            _context.SaveChanges();
            return true;
        }
    }
}
