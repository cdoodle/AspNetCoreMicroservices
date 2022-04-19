using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<ProductVm>
    {
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductVm>
    {
        private readonly IAsyncProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IAsyncProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ProductVm> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var allProducts = await _productRepository.GetAllAsync();

            return new ProductVm { Products = _mapper.Map<List<ProductDto>>(allProducts) };
        }
        
    }
}
