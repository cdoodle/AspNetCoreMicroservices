using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public class OrdersVm
    {
        public IList<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
