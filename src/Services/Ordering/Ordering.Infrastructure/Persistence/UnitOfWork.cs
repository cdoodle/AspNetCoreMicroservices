using Ordering.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderContext _context;
        private readonly IOrderRepository _repository;

        public UnitOfWork(OrderContext context, IOrderRepository repository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
        }
        public IOrderRepository Orders { get => _repository; }

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
