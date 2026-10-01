using Serilog.Context;
using System.Security.Claims;

namespace ECommerceApi.Middleware;

public sealed class UserContextLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            // Anonim sorgu (login, register) — tanimaq ucun bir sey yoxdur
            await next(context);
            return;
        }

        using (LogContext.PushProperty("UserId", userId))
        {
            await next(context);
        }
    }
}