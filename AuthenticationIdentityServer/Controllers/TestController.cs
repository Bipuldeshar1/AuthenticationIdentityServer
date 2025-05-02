using AuthenticationIdentityServer.Models.ViewModel;
using AuthenticationIdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationIdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TestController : ControllerBase
    {
        [HttpPost("test")]
        [Authorize]
        public async Task<IActionResult> test()
        {
           
            return Ok("accessed");
        }
    }
}
