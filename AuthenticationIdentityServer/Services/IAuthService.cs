using AuthenticationIdentityServer.Data;
using AuthenticationIdentityServer.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationIdentityServer.Services
{

    public interface IAuthService
    {
        Task<User> ValidateUserAsync(string email, string password);
        Task<User> RegisterUserAsync(string email, string? password = null);
        Task<User> ValidateUserViaEmailAsync(string email);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;

        public AuthService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<User> RegisterUserAsync(string email, string? password=null)
        {
            var user = new User();
            if (password == null) {
                 user = new User
                {
                    Email = email,
                };

            }
            else
            {
                user = new User
                {
                    Email = email,
                    Password = password
                };
            }
            
            await context.users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;

        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            var user= await context.users.FirstOrDefaultAsync(x=>x.Email==email);
            if (user == null) {
                return null;
            }
            if (user.Password != password) {
                return null;
            }
            return user;
        }

        public async Task<User> ValidateUserViaEmailAsync(string email)
        {
            var user = await context.users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

    }
}
