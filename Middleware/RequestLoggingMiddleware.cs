using Microsoft.AspNetCore.Http;

namespace UserManagementAPI.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log the incoming request details
        _logger.LogInformation("Incoming Request: {Method} {Path}", context.Request.Method, context.Request.Path);

        // Pass control to the next middleware in the pipeline
        await _next(context);

        // Log the outgoing response status code
        _logger.LogInformation("Outgoing Response: {StatusCode}", context.Response.StatusCode);
    }
}