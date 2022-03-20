using ForumBackEnd.Models;

namespace ForumBackEnd.Repositories
{
    public interface IUserRepository: IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);

        User GetUserByEmail(string email);
        void InsertUser(User user);
        void DeteleUser(int userId);
        void UpdateUser(User user);
        bool UserExists(int userId);
        void Save();
    }
}
