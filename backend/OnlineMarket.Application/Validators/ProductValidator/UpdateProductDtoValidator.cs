using FluentValidation;
using OnlineMarket.Application.DTOs;

namespace OnlineMarket.Application.Validators.ProductValidator;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(product => product.Id)
            .GreaterThan(0).WithMessage("Product id must be greater than zero.");

        RuleFor(product => product.Name)
            .Length(2, 50).WithMessage("Product name must be between 2 and 50 characters.");

        RuleFor(product => product.Code)
            .GreaterThan(0).WithMessage("Product code must be greater than zero.");

        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");
    }
}