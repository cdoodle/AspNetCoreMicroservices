using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDBContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("CatalogConnectionString"),
                        b => b.MigrationsAssembly(typeof(CatalogDBContext).Assembly.FullName)));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));
            services.AddScoped<IAsyncProductRepository, ProductRepository>();

            return services;
        }
    }
}
