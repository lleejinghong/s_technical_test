using System.Reflection;
using Azure.Storage.Blobs;
using LjhBackendApi.Application.Common.Interfaces;
using LjhBackendApi.Application.Contracts;
using LjhBackendApi.Domain.Constants;
using LjhBackendApi.Domain.Entities;
using LjhBackendApi.Infrastructure;
using LjhBackendApi.Infrastructure.Data;
using LjhBackendApi.Infrastructure.Data.Interceptors;
using LjhBackendApi.Infrastructure.Repository;
using LjhBackendApi.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddTransient<IApplicationUsersRepository, ApplicationUsersRepository>();
        services.AddTransient<IJWTManagerRepository, JWTManagerRepository>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services.AddSingleton(TimeProvider.System);

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));
        
        services.AddScoped<IBlobService>(sp => {
            var blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=your_account_name;AccountKey=your_account_key;EndpointSuffix=your_endpoint_suffix");
            return new BlobService(blobServiceClient, sp.GetRequiredService<ILogger<BlobService>>());
        });


        return services;
    }
}
