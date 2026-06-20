using UserManagementAPI.Services;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CRITICAL: Register the service as a Singleton so the list persists across requests
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

// --- PIPELINE CONFIGURATION ---
// 1. Error-handling middleware first
app.UseMiddleware<ExceptionHandlingMiddleware>();

// 2. Authentication middleware next
app.UseMiddleware<TokenAuthenticationMiddleware>();

// 3. Logging middleware last
app.UseMiddleware<RequestLoggingMiddleware>();
// ------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enables the Swagger UI at /swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
