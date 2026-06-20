using System.Net;
using System.Text.Json;

namespace UserManagementAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Proceed to the next middleware or controller
            await _next(context);
        }
        catch (Exception ex)
        {
            // Catch the error globally
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            Error = "An unexpected error occurred while processing your request.",
            // It is okay to show the exception message in a toy app, 
            // but remove 'Details' in a real production app for security!
            Details = exception.Message 
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}