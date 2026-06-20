using Microsoft.AspNetCore.Http;

namespace UserManagementAPI.Middleware;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    // For this assignment, we use a hardcoded valid token.
    private const string ValidToken = "TechHive-Secret-Token"; 

    public TokenAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Extract the token from the "Authorization" header
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        
        // Let's assume the format is "Bearer TechHive-Secret-Token" or just the token itself
        var token = authHeader?.Replace("Bearer ", "").Trim();

        if (string.IsNullOrEmpty(token) || token != ValidToken)
        {
            // If the token is invalid, return 401 and short-circuit the pipeline
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            
            await context.Response.WriteAsync("{\"error\": \"Unauthorized: Invalid or missing token.\"}");
            return; // Notice we do NOT call _next(context) here. The request stops.
        }

        // If the token is valid, proceed to the next middleware
        await _next(context);
    }
}