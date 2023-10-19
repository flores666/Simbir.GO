using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Simbir.GO.Services.Interfaces;

namespace Simbir.GO;

[Authorize]
public class TokenRefreshMiddleware
{
    private readonly RequestDelegate _next;
    
    public TokenRefreshMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITokenService tokenService)
    {
        var token = context.Request.Cookies["refreshToken"];
        if (token == null) context.User = null;
        await _next(context);
    }
}
