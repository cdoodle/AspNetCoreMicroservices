using Discount.Application.Models;
using Discount.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _service;

        public DiscountController(IDiscountService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("{productName}")]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CouponDto>> GetDiscount(string productName)
        {
            var coupon = await _service.GetAsync(productName);
            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Create([FromBody] CreateCouponDto coupon)
        {
            await _service.CreateAsync(coupon);
            return Ok();
        }

        [HttpPut("{couponId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Update(int couponId, [FromBody] UpdatedCouponDto coupon)
        {
            await _service.UpdateAsync(couponId, coupon);
            return Ok();
        }

        [HttpDelete("{productName}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            await _service.DeleteAsync(productName);

            return Ok();
        }
    }
}
