namespace SystemManage.DbContexts
{
    public class SystemContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
