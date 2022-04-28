using FluentValidation;


namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UpdatedOrder.UserName)
                .NotEmpty()
                .WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} should be less than 50 characters");

            RuleFor(p => p.UpdatedOrder.EmailAddress)
                .NotEmpty()
                .WithMessage("{EmailAddress} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{EmailAddress} should be less than 100 characters");

            RuleFor(p => p.UpdatedOrder.TotalPrice)
                .NotEmpty()
                .WithMessage("{TotalPrice} is required")
                .GreaterThan(0)
                .WithMessage("{TotalPrice} should be greater than 0");
        }
    }
}
