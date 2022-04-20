using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncProductRepository Products { get; }
        Task<int> CompleteAsAsync(CancellationToken cancellationToken);

    }
}
