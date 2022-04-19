using Application.Common.Interfaces;


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

        public int CompleteAsAsync()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
