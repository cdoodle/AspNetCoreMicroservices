using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetProducts
{
    public class GetProductByCategoryQuery : IRequest<ProductVm>
    {
        public string Category { get; set; }

        public GetProductByCategoryQuery(string category)
        {
            Category = category ?? throw new ArgumentNullException(nameof(Category));
        }
    }

    public class GetProductByCategoryQueryHandler : IRequestHandler<GetProductByCategoryQuery, ProductVm>
    {
        private readonly IAsyncProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByCategoryQueryHandler(IAsyncProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductVm> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductByCategory(request.Category);

            return new ProductVm { Products = _mapper.Map<List<ProductDto>>(products) };

        }
    }

}
