using Account.Models;

namespace SystemManage.Repository.Interface
{
    public interface IGORepository
    {
        void AddRole(Role role);
        void AddUser(User user);
        void ChangeUserRole(string userId, string roleId);
        void ChangeUserStatus(string userId);
        void DeleteRole(string roleId);
        IEnumerable<User> GetUsers();
        IEnumerable<Role> GetRoles();
        void ResetUserPassword(string userId);
        void UpdateRoleName(string roleId, string roleName);
    }
}
