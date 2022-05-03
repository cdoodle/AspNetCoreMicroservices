using AutoMapper;
using Basket.Application.Models;
using Basket.Application.Services.Grpc;
using Basket.Core.Entities;
using Basket.DataAccess.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Basket.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketService(IMapper mapper, IBasketRepository repository, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task CreateAsync(ShoppingCartDto shoppingCartDto)
        {
            var basket = _mapper.Map<ShoppingCart>(shoppingCartDto);

            await _repository.CreateAsync(basket);
        }

        public async Task DeleteAsync(string userName)
        {
            await _repository.DeleteAsync(userName);
        }

        public async Task<ShoppingCartDto> GetOrCreateBasketAsync(string userName)
        {
            var basket = await _repository.GetAsync(userName);
            if (basket is null)
            {
                throw new ArgumentNullException(userName, "Not found");
            }

            return _mapper.Map<ShoppingCartDto>(basket);
        }

        public async Task UpdateAsync(string userName, UpdateShoppingCartDto shoppingCartDto)
        {
            //Add Grpc service as "Connected Service"
            var basket = await _repository.GetAsync(userName);
            if (basket is null)
            {
                throw new ArgumentNullException(userName, "Not found");
            }

            //foreach (var item in shoppingCartDto.Items)
            //{
            //    var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            //    if (coupon.Amount < item.Price)
            //    {
            //        Console.WriteLine("Discount applied for... " + item.ProductName);
            //        item.Price -= coupon.Amount;
            //    }
            //}
            await applyDiscount(shoppingCartDto);

            _mapper.Map(shoppingCartDto, basket);

            await _repository.UpdateAsync(basket);
        }

        private async Task applyDiscount(IShoppingCartDto shoppingCartDto)
        {
            foreach (var item in shoppingCartDto.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                if (coupon.Amount < item.Price)
                {
                    Console.WriteLine("Discount applied for... " + item.ProductName);
                    item.Price -= coupon.Amount;
                }
            }
        }

        public async Task<int?> Checkout(BasketCheckout basketCheckout)
        {
            var basket = await _repository.GetAsync(basketCheckout.UserName);
            if (basket == null)
            {
                return null;
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;

            await _publishEndpoint.Publish(eventMessage);

            await _repository.DeleteAsync(basketCheckout.UserName);

            return 0;
        }
    }
}
