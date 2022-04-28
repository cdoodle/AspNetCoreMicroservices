using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Exceptions;
using Ordering.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateOrderDto UpdatedOrder { get; set; }

        public UpdateOrderCommand(int id, UpdateOrderDto orderDto)
        {
            Id = id;
            UpdatedOrder = orderDto;
        }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateOrderCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.Id);
            if (order == null)
            {
                //throw Exception
                throw new NotFoundException($"Order with Id {request.Id} not found");
            }

            _mapper.Map(request.UpdatedOrder, order);

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsAsync(cancellationToken);

            _logger.LogError("Order {request.Id} updated");
            return Unit.Value;
        }
    }
}
