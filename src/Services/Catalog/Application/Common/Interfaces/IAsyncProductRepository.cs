using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAsyncProductRepository: IAsyncRepository<Product>
   {
        void AddProduct(Product entity);

        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
    }
}
