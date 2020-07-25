using Microsoft.Extensions.DependencyInjection;

namespace Hcqn.Caching
{
    public static class NativeInjector
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, string connectionString)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
            });

            services.AddScoped<ICache, Cache>();

            return services;
        }
    }
}
