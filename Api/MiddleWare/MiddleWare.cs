using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Environments;
using Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;

namespace Api.MiddleWare;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMediator _mediator;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public AuthMiddleware(RequestDelegate next, ILoggerFactory logFactory, IMediator mediator,
        IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore)
    {
        _next = next;
        _mediator = mediator;
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");

        string token = context.Request.Headers["Authorization"];
        
        if (token != null)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_environmentsConfig.GetValue<string>(("JwtKey")));

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type.ToLower() == "id").Value;
                var username = jwtToken.Claims.First(x => x.Type.ToLower() == "username").Value;
                var claims = new[]
                {
                    new Claim("Id", userId),
                    new Claim("Username", username),
                };

                var val = await _memoryStore.GetAsync(userId, "access-token");

                if (!string.IsNullOrEmpty(val))
                {
                    var identity = new ClaimsIdentity(claims, "basic");
                    context.Items.Add("User",claims);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
     
        }

        await _next(context);
    }
}

public static class MyMiddlewareExtensions
{
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}