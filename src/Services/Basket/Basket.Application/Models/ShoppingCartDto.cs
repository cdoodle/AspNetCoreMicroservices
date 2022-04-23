using System;
using System.Collections.Generic;


namespace Basket.Application.Models
{
    public class ShoppingCartDto : IShoppingCartDto
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();

        public decimal TotalPrice { get; set; }

    }
}
