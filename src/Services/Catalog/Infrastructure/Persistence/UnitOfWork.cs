using Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDBContext _context;

        public UnitOfWork(CatalogDBContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
        }
        public IAsyncProductRepository Products { get; private set; }

        public async Task<int> CompleteAsAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
