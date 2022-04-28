using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand : IRequest<int>
    {
        public CheckoutOrderDto CheckoutOrder { get; set; }

        public CheckoutOrderCommand(CheckoutOrderDto orderDto)
        {
            CheckoutOrder = orderDto ?? throw new ArgumentNullException(nameof(orderDto));
        }
    }

    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request.CheckoutOrder);

            await _unitOfWork.Orders.AddAsync(order);

            await _unitOfWork.CompleteAsAsync(cancellationToken);
            
            _logger.LogInformation($"Order with Id {order.Id} created.");

            return order.Id;
        }
    }
}
