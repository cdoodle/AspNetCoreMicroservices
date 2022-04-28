using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        Task<int> CompleteAsAsync(CancellationToken cancellationToken);

    }
}
