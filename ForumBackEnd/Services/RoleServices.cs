using ForumBackEnd.Models;
using ForumBackEnd.Repositories;

namespace ForumBackEnd.Services
{
    public class RoleServices
    {
        // Se instacia la repo
        private IRoleRepository repository;

        public RoleServices(IRoleRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Role> FindRoles()
        {
            return repository.GetRoles();
        }

        public Role FindRoleById(int roleId)
        {
            return repository.GetRoleById(roleId);
        }

        public Role CreateRole(Role role)
        {
            
            repository.InsertRole(role);
            Role DbRole = repository.FindRoleByType(role.RoleType);
            return DbRole;
        }

        public bool RoleExists(int roleId)
        {
            return repository.RoleExists(roleId);
        }
    }
}
