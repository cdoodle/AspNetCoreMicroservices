using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<OrdersVm>
    {
        public string UserName { get; set; }

        public GetOrdersQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, OrdersVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task<OrdersVm> IRequestHandler<GetOrdersQuery, OrdersVm>.Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetOrdersByUserName(request.UserName);
            return new OrdersVm { Orders = _mapper.Map<List<OrderDto>>(orders) };
        }
    }
}
