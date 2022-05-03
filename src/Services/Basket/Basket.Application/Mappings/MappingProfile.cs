using AutoMapper;
using Basket.Application.Models;
using Basket.Core.Entities;
using EventBus.Messages.Events;

namespace Basket.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
            CreateMap<UpdateShoppingCartDto, ShoppingCart>();
            CreateMap<BasketCheckout, BasketCheckoutEvent>();
        }
    }
}
