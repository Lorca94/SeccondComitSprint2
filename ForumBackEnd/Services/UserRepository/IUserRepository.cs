using ForumBackEnd.Models;

namespace ForumBackEnd.Services.UserRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> FindAllUsers();
        User FindById(int userId);
        User FindByEmail(string email);
        User FindByUsername(string username);
        User Create(User user);
        void Update(User user); 
        bool DeleteById(int userId);
        bool ExistsByEmail(string email);
        bool UserExists(int userId);
    }
}
