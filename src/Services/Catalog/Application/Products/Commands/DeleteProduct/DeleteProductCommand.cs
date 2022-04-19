using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IAsyncProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IAsyncProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new ArgumentException(nameof(Product), request.Id.ToString());
            }

            await _productRepository.Remove(entity, cancellationToken);

            return Unit.Value;
        }
    }
}