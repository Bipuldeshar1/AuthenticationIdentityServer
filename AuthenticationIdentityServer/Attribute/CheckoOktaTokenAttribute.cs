using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationIdentityServer.Attribute
{
    public class CheckoOktaTokenAttribute:ActionFilterAttribute
    {
        public CheckoOktaTokenAttribute()
        {
          
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) { }
    }
}
