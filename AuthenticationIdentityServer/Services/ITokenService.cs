using AuthenticationIdentityServer.Data;
using AuthenticationIdentityServer.Models.Model;
using AuthenticationIdentityServer.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationIdentityServer.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }

    public class TokenService : ITokenService
    {
        private readonly AppDbContext context;

        public TokenService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<string> GenerateToken(User user)
        {

            var roles = await context.UserRoles
        .Where(ur => ur.UserId == user.Id)
        .Include(ur => ur.Role)
        .Select(ur => ur.Role.RoleName)
        .ToListAsync();


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SecreasdasdasdasdasdasdasdasdasdadasdsadadaasdsadasdasdastKey");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                    //add claims
                new Claim("Name", user.Email),
                new Claim(ClaimTypes.Role, roles[0]) 
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Audience = "yourAud",
                Issuer = "yourIss"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
