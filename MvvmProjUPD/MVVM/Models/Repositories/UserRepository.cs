using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MvvmProjUPD.MVVM.Models.Repositories
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(User user);
        void Edit(User user);
        void Remove(User user);
        User GetById(int id);
        User GetByLogin(string login);
        IEnumerable<User> GetByAll();
    }

    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            return _context.Users.Include(user => user.Orders).Include(user => user.UserRole).Any(user => user.UserLogin == credential.UserName && user.UserPassword == credential.Password);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Edit(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetByAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(user => user.UserID == id);
        }

        public User GetByLogin(string login)
        {
            return _context.Users.Include(u => u.Orders).FirstOrDefault(user => user.UserLogin == login);
        }

        public IEnumerable<Order> GetUserOrders(int userId)
        {
            return _context.Orders.Where(o => o.UserID == userId).ToList();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
