using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumBackEnd.Repositories
{
    public class RoleRepository: IRoleRepository, IDisposable
    {
        // Se instancia contexto
        private ForumBackEndContext context;

        // Constructor
        public RoleRepository(ForumBackEndContext context)
        {
            this.context = context;
        }

        // Se implementa IRoleRepository
        public IEnumerable<Role> GetRoles()
        {
            return context.Roles.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return context.Roles.Find(roleId);
        }

        public void InsertRole (Role role)
        {
            context.Roles.Add(role);
        }

        public void DeteleRole(int roleId)
        {
            Role role = context.Roles.Find(roleId);
            context.Roles.Remove(role);
        }

        public void UpdateUser(Role role)
        {
            context.Entry(role).State = EntityState.Modified;
        }
        
        public void AddUserToRole(User user, Role role)
        {
            role.Users.Add(user);
        }

        public void DeleteUserToRole(int userId, int roleId)
        {
            var role = context.Roles.Find(roleId);
            var user = context.Users.Find(userId);
            role.Users.Remove(user);
        }

        public Role FindRoleByType(string roleType)
        {
            Role role = context.Roles.Where(x => x.RoleType == roleType).FirstOrDefault();
            return role;
        }

        public bool RoleExists(int roleId)
        {
            return context.Roles.Any(role => role.Id == roleId);
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

