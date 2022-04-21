using System;
using System.Collections.Generic;


namespace Basket.Application.Models
{
    public class ShoppingCartDto
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();

    }
}
