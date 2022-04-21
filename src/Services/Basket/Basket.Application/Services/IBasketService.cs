
using Basket.Application.Models;
using System.Threading.Tasks;

namespace Basket.Application.Services
{
    public interface IBasketService
    {
        Task UpdateAsync(string userName, UpdateShoppingCartDto shoppingCartDto);
        Task CreateAsync(ShoppingCartDto shoppingCartDto);

        Task<ShoppingCartDto> GetOrCreateBasketAsync(string userName);
        Task DeleteAsync(string userName);
    }
}
