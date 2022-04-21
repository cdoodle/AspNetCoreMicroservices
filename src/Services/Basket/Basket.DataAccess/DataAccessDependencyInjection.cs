using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.DataAccess
{
    public static class DataAccessDependencyInjection
    {
        public static IServiceCollection AddRedisCacheAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnectionString");
            });
            return services;
        }
    }
}
