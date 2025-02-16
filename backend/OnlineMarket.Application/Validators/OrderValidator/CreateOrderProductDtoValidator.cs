using FluentValidation;
using OnlineMarket.Application.DTOs.OrderProductDto;

namespace OnlineMarket.Application.Validators;

public class CreateOrderProductDtoValidator : AbstractValidator<CreateOrderProductDto>
{
    public CreateOrderProductDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Product id must be greater than zero.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}