using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
    options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
});


// Add services to the container
builder.Services.AddControllers();

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Iron-Software",
            ValidAudience = "Iron-User",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Sample-Signing-Key-IronSoftware-Random-Number-c123sda3r24124fvb"))
        };
    });

var app = builder.Build();

// Ensure UseStaticFiles is enabled
app.UseStaticFiles();

// Map the "GeneratedPdfs" folder to be accessible via localhost
var basePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedPdfs");

if (!Directory.Exists(basePathDirectory))
{
    Directory.CreateDirectory(basePathDirectory);
}

// Add the static file middleware for the GeneratedPdfs directory
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(basePathDirectory),
    RequestPath = "/GeneratedPdfs" // This will map to localhost:5001/GeneratedPdfs
});

app.UseRouting(); // Ensure routing middleware is enabled

// Middleware for authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();