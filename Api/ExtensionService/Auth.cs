using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ExtensionService;

public class AuthTokenRequiredAttribute : TypeFilterAttribute
{
    public AuthTokenRequiredAttribute() : base(typeof(TokenAuthorizationFilter))
    {
    }
}

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class TokenAuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly IWebHostEnvironment _environment;
 
    public TokenAuthorizationFilter( IWebHostEnvironment environment)
    {

        _environment = environment;
    }
   
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var user = context.HttpContext.Items["User"];
        if (user == null)
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}