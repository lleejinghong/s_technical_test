using LjhBackendApi.Application.Common.Interfaces;
using LjhBackendApi.Domain.Constants;
using LjhBackendApi.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LjhBackendApi.Infrastructure.Data;
public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    //private readonly UserManager<ApplicationUser> _userManager;
    //private readonly RoleManager<IdentityRole> _roleManager;

    //public ApplicationDbContextInitialiser(
    //    ILogger<ApplicationDbContextInitialiser> logger,
    //    ApplicationDbContext context,
    //    UserManager<ApplicationUser> userManager,
    //    RoleManager<IdentityRole> roleManager)
    //{
    //    _logger = logger;
    //    _context = context;
    //    _userManager = userManager;
    //    _roleManager = roleManager;
    //}

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        //// Default roles
        //var administratorRole = new IdentityRole(Roles.Administrator);

        //if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        //{
        //    await _roleManager.CreateAsync(administratorRole);
        //}

        //// Default users
        //var administrator = new ApplicationUser { FirstName = "Jing Hong", LastName = "Lee", Email = "administrator@localhost" };

        //if (_userManager.Users.All(u => u.Email != administrator.Email))
        //{
        //    await _userManager.CreateAsync(administrator, "Administrator1!");
        //    if (!string.IsNullOrWhiteSpace(administratorRole.Name))
        //    {
        //        await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        //    }
        //}

        // Default user
        if (!_context.Users.Any())
        {
            _context.Users.AddRange(
                new ApplicationUser
                {
                    Email = "admin@localhost",
                    FirstName = "Jing Hong",
                    LastName = "Lee",
                    Password = "1234",
                    PhoneNumber = "1234567890",
                    ZipCode = "12345"
                },
                new ApplicationUser
                {
                    Email = "user1@example.com",
                    FirstName = "Alice",
                    LastName = "Tan",
                    Password = "password1",
                    PhoneNumber = "9876543210",
                    ZipCode = "54321"
                },
                new ApplicationUser
                {
                    Email = "user2@example.com",
                    FirstName = "Bob",
                    LastName = "Ng",
                    Password = "password2",
                    PhoneNumber = "5556667777",
                    ZipCode = "67890"
                }
            );

            await _context.SaveChangesAsync();
        }

    }
}
