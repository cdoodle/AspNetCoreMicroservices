using Basket.Application.Services;
using Basket.Application.Services.Grpc;
using Basket.DataAccess.Repositories;
using Discount.Grpc.Protos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Basket.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped<DiscountGrpcService>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new System.Uri(configuration["GrpcSettings:DiscountUrl"]));

            services.AddScoped<IBasketService, BasketService>();
            
            return services;
        }
    }
}
