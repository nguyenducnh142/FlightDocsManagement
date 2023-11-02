using LoginService.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginService.DbContexts
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
