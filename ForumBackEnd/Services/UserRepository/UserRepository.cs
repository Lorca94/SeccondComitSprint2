using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumBackEnd.Services.UserRepository
{
    public class UserRepository: IUserRepository
    {
        private ForumBackEndContext context;

        public UserRepository(ForumBackEndContext context)
        {
            this.context = context;
        }
        public IEnumerable<User> FindAllUsers()
        {
            return context.Users.ToList();
        }

        public User FindById(int userId)
        {
            return context.Users.Find(userId);
        }

        public User FindByEmail(string email)
        {
            return context.Users.Where( x => x.Email == email).FirstOrDefault();
        }

        public User FindByUsername(string username)
        {
            return context.Users.Where(x => x.Username == username).FirstOrDefault();
        }
        public User Create (User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
        public void Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }

        public bool DeleteById(int userId)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                return false;
            } else
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
        }

        public bool ExistsByEmail(string email)
        {
            return context.Users.Any( x => x.Email == email);
        }

        public bool UserExists(int userId)
        {
            return context.Users.Any(x => x.Id == userId);
        }
    }
}
