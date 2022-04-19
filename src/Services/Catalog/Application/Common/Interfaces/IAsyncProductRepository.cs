using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAsyncProductRepository: IAsyncRepository<Product>
    {
        Task<Guid> AddProductAsync(Product entity, CancellationToken cancellationToken);

        Task UpdateProductAsync(Product entity, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
    }
}
