using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IUserService
    {
        public User CreateUser(User user);
        public IEnumerable<User> GetUsers();
        public User GetUserById(int id);
        public bool UpdateUser(int id, User user);
        public bool DeleteUserById(int id);

    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly List<int> _deletedIds = []; // храним удаленные ID tables

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
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        // получаем юзера по id 
        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        // обновляем юзера по id и меняем его данные
        public bool UpdateUser(int id, User updatedUser)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return false;

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Phone = updatedUser.Phone;

            _context.SaveChanges();
            return true;
        }

        // удаляем юзера по id 
        public bool DeleteUserById(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);

            _context.SaveChanges();

            _deletedIds.Add(id);

            return true;
        }
    }
}
