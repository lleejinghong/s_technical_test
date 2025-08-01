using System.Text;
using LjhBackendApi.Domain.Entities;
using LjhBackendApi.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Create a new WebApplicationBuilder instance to configure the application
var builder = WebApplication.CreateBuilder(args);

// Service Configuration Section
#region Service Configuration

// Configure Azure Key Vault if specified in configuration
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

// Register services from different layers of the application
builder.Services.AddApplicationServices();    // Core application services
builder.Services.AddInfrastructureServices(builder.Configuration);  // Infrastructure services
builder.Services.AddWebServices();            // Web-specific services
builder.Services.AddHttpClient();             // HTTP client factory

// Configure CORS to allow all origins, methods and headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// JWT Authentication Configuration
var jwtSettings = builder.Configuration.GetSection("JWT");
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => {
    var Key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ClockSkew = TimeSpan.Zero      // No tolerance for token expiration time
    };
});

//builder.Services.AddIdentity<HelperIdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

// Add authorization services
builder.Services.AddAuthorization();

#endregion

// Build the application
var app = builder.Build();




#region Middleware Pipeline

// Enable CORS with the "AllowAll" policy
app.UseCors("AllowAll");

// Configure environment-specific middleware
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();  // Initialize the database in development
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Authentication & Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Infrastructure middleware
app.UseHealthChecks("/health");          // Enable health check endpoint
app.UseHttpsRedirection();               // Redirect HTTP to HTTPS
app.UseStaticFiles();                    // Enable serving static files


// Configure Swagger UI endpoint
app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

// Configure routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

// SPA fallback
app.MapFallbackToFile("index.html");

// Global exception handler
app.UseExceptionHandler(options => { });

// Redirect root to API documentation
app.Map("/", () => Results.Redirect("/api"));

// Register additional endpoints
app.MapEndpoints();

#endregion

// Start the application
app.Run();

public partial class Program { }
