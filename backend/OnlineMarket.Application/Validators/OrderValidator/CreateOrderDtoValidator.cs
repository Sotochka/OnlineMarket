using FluentValidation;
using OnlineMarket.Application.DTOs.OrderDtos;

namespace OnlineMarket.Application.Validators;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(order => order.UserId)
            .GreaterThan(0).WithMessage("User id must be greater than zero.");

        RuleFor(order => order.CustomerFullName)
            .NotEmpty().WithMessage("Customer name is required.")
            .Length(2, 50).WithMessage("Customer name must be between 2 and 100 characters.");

        RuleFor(order => order.CustomerPhone)
            .NotEmpty().WithMessage("Customer phone is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Customer phone must be a valid phone number.");

        RuleFor(order => order.OrderProducts)
            .NotEmpty().WithMessage("Order products are required.")
                .ForEach(product => product.SetValidator(new CreateOrderProductDtoValidator()));
    }
}