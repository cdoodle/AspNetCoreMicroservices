using Discount.Application.Models;
using System.Threading.Tasks;

namespace Discount.Application.Services
{
    public interface IDiscountService
    {
        Task UpdateAsync(int couponId, UpdatedCouponDto couponDto);
        Task CreateAsync(CreateCouponDto couponDto);

        Task<CouponDto> GetAsync(string userName);
        Task DeleteAsync(string userName);
    }
}
