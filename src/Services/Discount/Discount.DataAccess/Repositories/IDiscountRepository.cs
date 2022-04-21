using System.Threading.Tasks;
using Discount.Core.Entities;

namespace Discount.DataAccess.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<Coupon> GetDiscountById(int couponId);
        
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
    }
}
