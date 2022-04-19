using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetProducts
{

    public class GetProductByIdQuery : IRequest<ProductVm>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVm>
    {
        private readonly IAsyncProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IAsyncProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.SingleOrDefaultAsync(p => p.Id == request.Id);

            return new ProductVm { Products = new List<ProductDto> { _mapper.Map<ProductDto>(product) } };
        }
    }
}
