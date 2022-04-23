using System.Collections.Generic;

namespace Basket.Application.Models
{
    public interface IShoppingCartDto
    {
        public List<ShoppingCartItemDto> Items { get; set; }
    }
}
