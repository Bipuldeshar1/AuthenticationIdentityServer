using AuthenticationIdentityServer.Models.ViewModel;
using AuthenticationIdentityServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationIdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Login(UserVm user)
        {
             await authService.RegisterUserAsync(user.Email, user.Password);
             return Ok();
        }
    }
}
