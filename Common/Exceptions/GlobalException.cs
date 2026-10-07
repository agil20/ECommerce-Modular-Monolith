using Common.Models;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
namespace Common.Exceptions;

public class GlobalException(ILogger<GlobalException> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        int statuscode = exception switch
        {
            NotFoundException =>(int) HttpStatusCode.NotFound,
            DublicatedDataException =>(int) HttpStatusCode.BadRequest,
            ConfilictException =>(int) HttpStatusCode.BadRequest,
            UnauthorizedException =>(int) HttpStatusCode.Unauthorized,
            _=>(int)HttpStatusCode.InternalServerError

        };
        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonim";

        if (statuscode >= 500)
            logger.LogError(exception, "Gozlenilmeyen xeta. UserId: {UserId}, Path: {Path}",
                userId, httpContext.Request.Path);
        else
            logger.LogWarning("Biznes xetasi: {ExceptionType}. UserId: {UserId}, Path: {Path}",
                exception.GetType().Name, userId, httpContext.Request.Path);
        var response = new ApiResponseModel
            (
            false,statuscode,exception.Message
            );

        httpContext.Response.StatusCode=statuscode;
        await httpContext.Response.WriteAsJsonAsync(response,cancellationToken);
        return true;
    }
}
