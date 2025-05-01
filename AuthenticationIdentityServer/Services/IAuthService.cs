using AuthenticationIdentityServer.Data;
using AuthenticationIdentityServer.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationIdentityServer.Services
{
   
    public interface IAuthService
    {
        Task<User> ValidateUserAsync(string email, string password);
        Task RegisterUserAsync(string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;

        public AuthService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task RegisterUserAsync(string email, string password)
        {
            var user = new User
            {
                Email=email,
                Password=password
            };
            await context.users.AddAsync(user);
            await context.SaveChangesAsync();

        }

        public Task<User> ValidateUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
