using Basket.Core.Entities;
using System.Threading.Tasks;

namespace Basket.DataAccess.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetAsync(string userName);
        Task CreateAsync(ShoppingCart basket);

        Task UpdateAsync(ShoppingCart basket);
        Task DeleteAsync(string userName);
    }
}
