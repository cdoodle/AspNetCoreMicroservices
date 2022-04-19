
using System.Collections.Generic;

namespace Application.Products.Queries.GetProducts
{
    public class ProductVm
    {
        public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
