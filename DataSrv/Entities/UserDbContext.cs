using Microsoft.EntityFrameworkCore;

namespace DataSrv.Entities
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options):base(options) { }

        public DbSet<UserDetails> userDetails { get; set; }

    }
}
