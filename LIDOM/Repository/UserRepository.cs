using LIDOM.Databases;
using LIDOM.Interface;
using LIDOM.Models;
using LIDOM.utils;

namespace LIDOM.Repository
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly LidomDBContext _context;
        private readonly PasswordHasher _passwordHasher;
        public UserRepository(PasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _context = new LidomDBContext();
        }

        public User? login(string userName, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName);

            if(user == null)
            {
                return null;
            }    

            if(_passwordHasher.VerifyPassword(password, user.Password))
            {
                return user;
            }

            return null;
        }

        public bool register(string userName, string password)
        {
            var existsUser = _context.Users.SingleOrDefault(u => u.UserName == userName);
            if (existsUser != null)
            {
                return false;
            }

            User user = new User()
            {
                UserName = userName,
                Password = password,
            };
            
            _context.Users.Add(user);
            var affected = _context.SaveChanges();
            if (affected != 1) return false;

            return true;
        }
    }
}
