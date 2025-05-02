using AuthenticationIdentityServer.Models.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationIdentityServer.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Role> roles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoles>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.userRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.userRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin" },
                new Role { Id = 2, RoleName = "User" }
            );

            // Seed a default user (example)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    Email = "admin@example.com",
                    Password = "Admin@123" 
                }
            );

            // Seed user-role relationship
            modelBuilder.Entity<UserRoles>().HasData(
                new UserRoles { UserId = 2, RoleId = 1 } 
            );
        }
    }
}
