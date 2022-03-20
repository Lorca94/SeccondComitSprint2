using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumBackEnd.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        // Se intancia el contexto
        private ForumBackEndContext context;

        // Constructor
        public UserRepository(ForumBackEndContext context)
        {
            this.context = context;
        }

        // Metodos

        // Devuelve todos los usuarios en BBDD
        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        // Busca un usuario por ID, y devuelve un usuario de BBDD
        public User GetUserById(int userId)
        {
            return context.Users.Find(userId);
        }

        // Busca un usuario por email
        public User GetUserByEmail(string email)
        {
            return context.Users.Where(r => r.Email == email).FirstOrDefault();
        }

        // Inserta un usuario en BBDD
        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }

        // Elimina un usuario de BBDD
        public void DeteleUser(int userId)
        {
            User user = context.Users.Find(userId);
            context.Users.Remove(user);
        }

        // Actualiza un usuario
        public void UpdateUser(User user)
        {
            try
            {
                context.Entry(user).State = EntityState.Modified;
            } catch
            {

            }
        }

        public bool UserExists(int userId)
        {
            return context.Users.Any(user => user.Id == userId);
        }
 
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
