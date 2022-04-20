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

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.Product);

            _unitOfWork.Products.AddProduct(product);
            await _unitOfWork.CompleteAsAsync(cancellationToken);
            
            return product.Id;
        }
    }
}
