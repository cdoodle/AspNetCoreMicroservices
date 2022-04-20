using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ProductRepository : Repository<Product>, IAsyncProductRepository
    {
        public ProductRepository(CatalogDBContext context) : base(context)
        {
        }

        public void AddProduct(Product entity)
        {
            Context.Set<Product>().Add(entity);
            //await Context.SaveChangesAsync(cancellationToken);
            //return entity.Id;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return await Context.Set<Product>().Where(p => p.Category == categoryName).ToListAsync();
        }
    }
}
