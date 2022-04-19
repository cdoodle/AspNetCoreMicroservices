using Application.Common.Interfaces;
using Application.Products.Queries.GetProducts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public ProductDto Product { get; set; }

        public CreateProductCommand(ProductDto product)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {

        private readonly IAsyncProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IAsyncProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.Product);
            await _productRepository.AddProductAsync(product, cancellationToken);

            return product.Id;

        }
    }
}
