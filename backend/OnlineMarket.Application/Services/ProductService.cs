﻿using OnlineMarket.Application.DTOs;
using OnlineMarket.Application.Interfaces;
using OnlineMarket.Application.Interfaces.Services;
using OnlineMarket.Domain;
using OnlineMarket.Domain.Entities;

namespace OnlineMarket.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<Result<IEnumerable<ProductDto>>> GetProductsAsync()
    {
        var products = await productRepository.GetAllProductsAsync();

        return products.IsSuccess ? Result<IEnumerable<ProductDto>>.Success(products.Value)
            : Result<IEnumerable<ProductDto>>.Failure(products.ErrorMessage);
    }

    public async Task<Result<ProductDto>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        return product.IsSuccess ? Result<ProductDto>.Success(product.Value) : Result<ProductDto>.Failure(product.ErrorMessage);
    }

    public async Task<Result<ProductDto>> GetProductByCodeAsync(int code)
    {
        var product = await productRepository.GetByCodeAsync(code);

        return product.IsSuccess ? Result<ProductDto>.Success(product.Value)
            : Result<ProductDto>.Failure(product.ErrorMessage);
    }

    public async Task<Result> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Code = createProductDto.Code,
            Name = createProductDto.Name,
            Price = createProductDto.Price
        };

        await productRepository.CreateAsync(product);

        return Result.Success();
    }

    public async Task<Result> UpdateProductAsync(UpdateProductDto product)
    {
        await productRepository.UpdateAsync(product);

        return Result.Success();
    }
}