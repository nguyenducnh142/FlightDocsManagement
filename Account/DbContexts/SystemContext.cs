using Account.Models;
using Microsoft.EntityFrameworkCore;

namespace SystemManage.DbContexts
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
