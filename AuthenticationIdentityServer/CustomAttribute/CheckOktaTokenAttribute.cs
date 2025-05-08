using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationIdentityServer.CustomAttribute
{
    public class CheckOktaTokenAttribute : Attribute, IAuthorizationFilter
    {
        public CheckOktaTokenAttribute()
        {
            
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var configuration = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var isValid = await ValidateToken(token,configuration);
            if (!isValid)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }


        private async Task<bool> ValidateToken(string token,IConfiguration configuration)
        {
            var handler = new JwtSecurityTokenHandler();
            var validationParams = new TokenValidationParameters
            {
                ValidIssuer = configuration["Okta:issuer"],
                ValidAudience = configuration["Okta:Audience"],
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
            };

            try
            {
                 handler.ValidateToken(token, validationParams, out _);
         
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}

