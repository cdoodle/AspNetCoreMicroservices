using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProducts;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ForMember(d => d.Id, o => o.Ignore());
            //CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            //CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
