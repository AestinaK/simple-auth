using Microsoft.EntityFrameworkCore;
using simple_authentication.Entity;

namespace simple_authentication.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
    }
}
