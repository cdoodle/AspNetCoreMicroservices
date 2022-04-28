using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.CheckoutOrder.UserName)
                .NotEmpty()
                .WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} should be less than 50 characters");

            RuleFor(p => p.CheckoutOrder.EmailAddress)
                .NotEmpty()
                .WithMessage("{EmailAddress} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{EmailAddress} should be less than 100 characters");

            RuleFor(p => p.CheckoutOrder.TotalPrice)
                .NotEmpty()
                .WithMessage("{TotalPrice} is required")
                .GreaterThan(0)
                .WithMessage("{TotalPrice} should be greater than 0");

        }
    }
}
