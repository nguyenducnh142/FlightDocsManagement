using Account.Models;
using Microsoft.EntityFrameworkCore;
using SystemManage.DbContexts;
using SystemManage.Repository.Interface;

namespace SystemManage.Repository.Repositories
{
    public class GORepository : IGORepository
    {
        private readonly SystemContext _dbContext;
        public GORepository(SystemContext context)
        {
            _dbContext = context;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void AddRole(Role role)
        {
            _dbContext.Roles.Add(role);
            Save();
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            Save();
        }

        public void ChangeUserRole(string userId, string roleId)
        {   
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var role = _dbContext.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            user.RoleId = roleId;
            Save();
        }

        public void ChangeUserStatus(string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.Status == true)
            {
                user.Status = false;
            }
            else
            {
                user.Status = true;
            }
            Save();
        }

        public void DeleteRole(string roleId)
        {
                var role = _dbContext.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            _dbContext.Roles.Remove(role);
            Save();
        }

        public void ResetUserPassword(string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.Password = "123456";
            Save();
        }

        public void UpdateRoleName(string roleId, string roleName)
        {
            var role = _dbContext.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            role.RoleName = roleName;
            Save();
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public IEnumerable<Role> GetRoles()
        {
            return _dbContext.Roles.ToList();
        }
    }
}
