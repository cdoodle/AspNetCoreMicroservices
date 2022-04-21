using Basket.Application.Services;
using Basket.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Basket.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            
            return services;
        }
    }
}
