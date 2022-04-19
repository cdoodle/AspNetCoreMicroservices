using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ProductRepository : Repository<Product>, IAsyncProductRepository
    {
        public ProductRepository(CatalogDBContext context): base(context)
        {
        }

        public async Task<Guid> AddProductAsync(Product entity, CancellationToken cancellationToken)
        {
            Context.Set<Product>().Add(entity);
            await Context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task UpdateProductAsync(Product entity, CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return await Context.Set<Product>().Where(p => p.Category == categoryName).ToListAsync();
        }
    }
}
