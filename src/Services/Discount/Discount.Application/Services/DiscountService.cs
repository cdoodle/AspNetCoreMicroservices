using AutoMapper;
using Discount.Application.Models;
using Discount.Core.Entities;
using Discount.DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace Discount.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _repository;

        public DiscountService(IDiscountRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateAsync(CreateCouponDto couponDto)
        {
            var coupon = _mapper.Map<Coupon>(couponDto);

            await _repository.CreateDiscount(coupon);
        }

        public async Task DeleteAsync(string userName)
        {
            await _repository.DeleteDiscount(userName);
        }

        public async Task<CouponDto> GetAsync(string productName)
        {
            var coupon = await _repository.GetDiscount(productName);
            if (coupon is null)
            {
                throw new ArgumentNullException(productName, "Not found");
            }

            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task UpdateAsync(int couponId, UpdatedCouponDto couponDto)
        {
            var coupon = await _repository.GetDiscountById(couponId);
            if (coupon is null)
            {
                throw new ArgumentNullException(couponId.ToString(), "Not found");
            }

            _mapper.Map(couponDto, coupon);
            
            await _repository.UpdateDiscount(coupon);
        }
    }
}
