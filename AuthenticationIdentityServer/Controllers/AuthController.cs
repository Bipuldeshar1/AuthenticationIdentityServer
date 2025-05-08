using System.Security.Claims;
using AuthenticationIdentityServer.CustomAttribute;
using AuthenticationIdentityServer.Models.ViewModel;
using AuthenticationIdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace AuthenticationIdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ITokenService tokenService;
        private static readonly IConfiguration config;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            this.authService = authService;
            this.tokenService = tokenService;
         
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserVm userModel)
        {
            await authService.RegisterUserAsync(userModel.Email, userModel.Password);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserVm userModel)
        {
            var verifiedUser = await authService.ValidateUserAsync(userModel.Email, userModel.Password);
            if (verifiedUser == null)
            {
                return BadRequest();
            }
            var token = await tokenService.GenerateToken(verifiedUser);

            return Ok(token);
        }















        //[HttpPost("/okta-login")]
        //[CheckOktaTokenAttribute]
        //public async Task<IActionResult> OktaLogin()
        //{
        //    var accesstoken = "";
        //    var email = HttpContext.User.FindFirst(ClaimTypes.Email);
   

        //    if (email == null)
        //    {
        //        return BadRequest("email not found in claims");
        //    }
        //    var user = await authService.ValidateUserViaEmailAsync(email.ToString());
        //    if (user == null)
        //    {
        //        //adding user to db
        //        var regUser = await authService.RegisterUserAsync(user!.Email);
        //        accesstoken = await tokenService.GenerateToken(regUser);
        //        return Ok(accesstoken);
        //    }
            
        //    accesstoken = await tokenService.GenerateToken(user);

        //    return Ok(accesstoken);
        //}


    }
}
