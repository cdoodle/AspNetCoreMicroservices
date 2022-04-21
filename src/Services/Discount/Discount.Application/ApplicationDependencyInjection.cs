using Discount.Application.Services;
using Discount.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Discount.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IDiscountService, DiscountService>();
            
            return services;
        }
    }
}
