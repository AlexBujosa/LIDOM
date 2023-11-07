using LIDOM.Models;

namespace LIDOM.Interface
{
    public interface IUserRepository<T>
    {
        public T login(string user, string password);

        public bool register(string user, string password);
    }
}
