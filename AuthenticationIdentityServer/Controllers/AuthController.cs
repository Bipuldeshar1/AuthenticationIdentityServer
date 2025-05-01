using AuthenticationIdentityServer.Models.ViewModel;
using AuthenticationIdentityServer.Services;
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

        public AuthController(IAuthService authService,ITokenService tokenService)
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
            var token =  tokenService.GenerateToken(verifiedUser);

            return Ok(token);
        }
    }
}
