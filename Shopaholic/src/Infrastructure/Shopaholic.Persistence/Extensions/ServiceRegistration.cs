using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopaholic.Application.Abstraction.Repositories;
using Shopaholic.Domain.Identity;
using Shopaholic.Persistence.Context;
using Shopaholic.Persistence.Implementations.Repositories;

namespace Shopaholic.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopaholicDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<ShopaholicDbContextInitialiser>();
        }
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                // Password settings.
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequiredLength = 8;
                opts.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.Lockout.AllowedForNewUsers = true;

                // User settings.
                opts.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opts.User.RequireUniqueEmail = true;
            }).AddDefaultTokenProviders()
              .AddEntityFrameworkStores<ShopaholicDbContext>();
        }
        public static void AddWriteRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IWriteRepository<>),(typeof(WriteRepository<>)));
        }
        public static void AddReadRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadRepository<>), (typeof(ReadRepository<>)));
        }
    }
}
