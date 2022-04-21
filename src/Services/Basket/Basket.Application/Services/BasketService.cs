using AutoMapper;
using Basket.Application.Models;
using Basket.Core.Entities;
using Basket.DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace Basket.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;

        public BasketService(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            var basket = await _repository.GetAsync(userName);
            if (basket is null)
            {
                throw new ArgumentNullException(userName, "Not found");
            }

            _mapper.Map(shoppingCartDto, basket);
            Console.WriteLine(basket.UserName);
            await _repository.UpdateAsync(basket);
        }
    }
}
