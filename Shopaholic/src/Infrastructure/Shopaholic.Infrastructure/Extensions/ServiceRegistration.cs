using Microsoft.Extensions.DependencyInjection;
using Shopaholic.Application.Abstraction.Services;
using Shopaholic.Infrastructure.Services;

namespace Shopaholic.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {
            // Register JWT Service
            services.AddScoped<IJWTService, JWTService>();
        }
    }
}
