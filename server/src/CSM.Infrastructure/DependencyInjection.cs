using System.Text;
using CSM.Core.Features.Channels;
using CSM.Core.Features.Countries;
using CSM.Core.Features.ErrorMessages;
using CSM.Core.Features.Roles;
using CSM.Core.Features.Users;
using CSM.Core.Features.Views;
using CSM.Infrastructure.Application;
using CSM.Infrastructure.Authentication;
using CSM.Infrastructure.Database;
using CSM.Infrastructure.Interceptors;
using CSM.Infrastructure.Repositories;
using CSM.Infrastructure.Seeds;
using CSM.UseCases.Abstractions.Application;
using CSM.UseCases.Abstractions.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CSM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddInterceptors()
            .AddMigrationService()
            .AddAuthenticationInternal(configuration)
            .AddAuthorizationInternal();
    
    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddHttpContextAccessor();
        
        services.AddScoped<IUserProvider, UserProvider>();
        services.AddScoped<ITransformer, Transformer>();
        
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        return services;
    }
    
    private static IServiceCollection AddAuthorizationInternal(
        this IServiceCollection services)
    {
        services.AddAuthorization();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContextPool<ApplicationDbContext>(
            (sp, options) => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<UpdateAuditableInterceptor>()));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IViewRepository, ViewRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IErrorMessageRepository, ErrorMessageRepository>()
            .AddScoped<IChannelRepository, ChannelRepository>()
            .AddScoped<ICountryRepository, CountryRepository>()
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
    
    private static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services
            .AddSingleton<UpdateAuditableInterceptor>();

        return services;
    }
    
    private static IServiceCollection AddMigrationService(this IServiceCollection services)
    {
        services
            .AddScoped<SeedViewData>();

        return services;
    }
}