using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;

        if (ex is DbUpdateException)
        {
            code = HttpStatusCode.InternalServerError;
        }

        if (ex is DbUpdateConcurrencyException)
        {
            code = HttpStatusCode.Conflict;
        }

        if (ex is ArgumentNullException)
        {
            code = HttpStatusCode.BadRequest;
        }

        if (ex is UnauthorizedAccessException)
        {
            code = HttpStatusCode.Forbidden;
        }

        if (ex is NotFoundException) code = HttpStatusCode.NotFound;

        if (ex is BadRequestException) code = HttpStatusCode.BadRequest;

        var result = JsonSerializer.Serialize(new { error = ex.Message });
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)code;
        return httpContext.Response.WriteAsync(result);
    }

}

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandler>();
    }
}
