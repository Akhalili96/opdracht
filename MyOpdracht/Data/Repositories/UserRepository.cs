using MyOpdracht.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOpdracht.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MyOpdrachtContext _context;
        public UserRepository(MyOpdrachtContext context)
        {
            _context = context;

        }
        public void AddUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public Users GetUserForLogin(string email, string password)
        {
            return _context.Users
                .SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool IsExistUserByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
