using System;
using System.Collections.Generic;


namespace Basket.Application.Models
{
    public class UpdateShoppingCartDto : IShoppingCartDto
    {
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    }
}
