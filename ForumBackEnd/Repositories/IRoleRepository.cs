using ForumBackEnd.Data;
using ForumBackEnd.Models;

namespace ForumBackEnd.Repositories
{
    public interface IRoleRepository: IDisposable
    {
        IEnumerable<Role> GetRoles();
        Role GetRoleById(int roleId);
        void InsertRole(Role role);
        void DeteleRole(int roleId);
        void UpdateUser(Role role);
        void AddUserToRole(User user, Role role);
        void DeleteUserToRole(int userId, int roleId);
        Role FindRoleByType(string roleType);
        bool RoleExists(int roleId);
        void Save();
    }
}
