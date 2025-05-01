using AuthenticationIdentityServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationIdentityServer.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }   
      
    }
}
