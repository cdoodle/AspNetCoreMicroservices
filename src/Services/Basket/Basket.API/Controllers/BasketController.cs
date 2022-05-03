using Basket.Application.Models;
using Basket.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingCartDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartDto>> GetBasket(string userName)
        {
            var basket = await _basketService.GetOrCreateBasketAsync(userName);
            return Ok(basket ?? new ShoppingCartDto { UserName = userName });
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> RemoveBasket(string userName)
        {
            await _basketService.DeleteAsync(userName);
            return Ok();
        }

        [HttpPut("{userName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateBasket(string userName, [FromBody] UpdateShoppingCartDto shoppingCartDto)
        {
            await _basketService.UpdateAsync(userName, shoppingCartDto);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateBasket([FromBody] ShoppingCartDto shoppingCartDto)
        {
            await _basketService.CreateAsync(shoppingCartDto);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _basketService.Checkout(basketCheckout);
            if (basket == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
