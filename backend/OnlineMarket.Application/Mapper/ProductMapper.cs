using OnlineMarket.Application.DTOs;
using OnlineMarket.Domain.Entities;

namespace OnlineMarket.Application.Mapper;

public static class ProductMapper
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        (
            product.Id,
            product.Code,
            product.Name,
            product.Price
        );
    }

    public static Product ToEntity(this CreateProductDto createProductDto)
    {
        return new Product
        {
            Code = createProductDto.Code,
            Name = createProductDto.Name,
            Price = createProductDto.Price
        };
    }

    public static Product ToEntity(this UpdateProductDto updateProductDto)
    {
        return new Product
        {
            Code = updateProductDto.Code ?? 0,
            Name = updateProductDto.Name,
            Price = updateProductDto.Price ?? 0
        };
    }
}