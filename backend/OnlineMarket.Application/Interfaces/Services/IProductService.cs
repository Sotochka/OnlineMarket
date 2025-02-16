using OnlineMarket.Application.DTOs;
using OnlineMarket.Domain;

namespace OnlineMarket.Application.Interfaces.Services;

public interface IProductService
{
    public Task<Result<IEnumerable<ProductDto>>> GetProductsAsync();
    public Task<Result<ProductDto>> GetProductByIdAsync(int id);
    public Task<Result<ProductDto>> GetProductByCodeAsync(int code);
    public Task<Result> CreateProductAsync(CreateProductDto product);
    public Task<Result> UpdateProductAsync(UpdateProductDto product);
}