using OnlineMarket.Application.DTOs;
using OnlineMarket.Domain;
using OnlineMarket.Domain.Entities;

namespace OnlineMarket.Application.Interfaces;

public interface IProductRepository
{
    Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync();
    Task<Result<ProductDto>> GetByIdAsync(int id);
    Task<Result<ProductDto>> GetByCodeAsync(int code);
    Task<Result> UpdateAsync(Product product);
    Task<Result> CreateAsync(Product product);
}