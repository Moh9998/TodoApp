
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using System.Text;
using TodoApp.Application.Abstractions;
using TodoApp.Domain.Users;
using TodoApp.Infrastructure.Identity;
using TodoApp.Infrastructure.Persistence;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var conn = config.GetConnectionString("Database")
                  ?? "Host=localhost;Port=5432;Database=appdb;Username=postgres;Password=123";

        services.AddDbContext<ApplicationContext>(o =>
        {
            o.UseNpgsql(conn, npg => npg.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
        });
        services.AddScoped<ITodoRepository, TodoRepository>( );
        services.AddScoped<IUnitOfWork, EfUnitOfWork>( );

        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdmin", p => p.RequireRole("Admin"));
        });

        services.AddHttpClient("geo", c =>
        {
            c.BaseAddress = new Uri("http://ip-api.com/");
            c.Timeout = TimeSpan.FromSeconds(10);
        })
        .SetHandlerLifetime(TimeSpan.FromMinutes(5))
        .AddPolicyHandler(HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retry => TimeSpan.FromMilliseconds(200 * (retry + 1))));

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
