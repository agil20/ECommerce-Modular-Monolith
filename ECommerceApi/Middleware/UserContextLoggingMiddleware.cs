using Serilog;
using Serilog.Context;
using System.Security.Claims;

namespace ECommerceApi.Middleware;

public sealed class UserContextLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IDiagnosticContext diagnosticContext)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            await next(context);
            return;
        }

        diagnosticContext.Set("UserId", userId);          // ← sorğu xülasəsi üçün

        using (LogContext.PushProperty("UserId", userId))  // ← biznes logları üçün
        {
            await next(context);
        }
    }
}