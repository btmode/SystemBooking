using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IUserService
    {
        public User CreateUser(User user);
        public IEnumerable<User> GetUsers();
        public User GetUserByName(string name);
        public User GetUserById(Guid id);
        public bool UpdateUser(Guid id, User user);
        public bool DeleteUserById(Guid id);

    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // создаем нового юзера
        public User CreateUser(User user)
        {
            _context.Users.Add(user);

            _context.SaveChanges();
            return user;
        }

        // получаем всех юзеров сразу
        public IEnumerable<User> GetUsers()
        {
            //return _context.Users.OrderBy(u => u.Id).Skip(2).Take(1).ToList();
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        
        public User GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(u=>u.Name == name);                 
        }

        public User GetUserById(Guid id)
        {
            return _context.Users.Find(id);
        }

        // обновляем юзера по id и меняем его данные
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

        // удаляем юзера по id 
        public bool DeleteUserById(Guid id)
        {
            var user = _context.Users.Find(id);

            if (user == null)  return false;

            _context.Users.Remove(user);

            _context.SaveChanges();
            return true;
        }
    }
}
