using System;

namespace Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncProductRepository Products { get; }
        int CompleteAsAsync();
    }
}
