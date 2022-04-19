using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public Guid Id { get; set; }

        public UpdateProductDto Product { get; set; }

        public UpdateProductCommand(Guid id, UpdateProductDto product)
        {
            Id = id;
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {

        private readonly IAsyncProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IAsyncProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async  Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new ArgumentException(nameof(Product), request.Id.ToString());
            }

            //entity = _mapper.Map<(UpdateProductDto)request.Product, entity>(request.Product, entity);
            //Return the same entity instead of creating a new mapped entity
            _mapper.Map(request.Product, entity);
            
            await _productRepository.UpdateProductAsync(entity, cancellationToken);

            return Unit.Value;

        }
    }
}
