using Dapper;
using OnlineMarket.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Domain.Entities;
using System.Data;
using OnlineMarket.Application.DTOs;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Domain;
using OnlineMarket.Infrastructure.SqlScripts;

namespace OnlineMarket.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly OnlineMarketDbContext _context;
    private readonly IDbConnection _connection;

    public ProductRepository(OnlineMarketDbContext context)
    {
        _context = context;
        _connection = _context.Database.GetDbConnection();
    }

    public async Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync()
    {
        var sql = ProductSqlScripts.GetAllProducts;

        var products = await _connection.QueryAsync<ProductDto>(sql);

        return products == null
            ? Result<IEnumerable<ProductDto>>.Failure("No products found")
            : Result<IEnumerable<ProductDto>>.Success(products);
    }

    public async Task<Result<ProductDto>> GetByIdAsync(int id)
    {
        var sql = ProductSqlScripts.GetProductById;

        var product = await _connection.QueryFirstOrDefaultAsync<ProductDto>(sql, new { Id = id });

        return product == null
            ? Result<ProductDto>.Failure($"Product with Id {id} not found")
            : Result<ProductDto>.Success(product);
    }

    public async Task<Result<ProductDto>> GetByCodeAsync(int code)
    {
        var sql = ProductSqlScripts.GetProductByCode;

        var product = await _connection.QueryFirstOrDefaultAsync<ProductDto>(sql, new { Code = code });

        return product == null
            ? Result<ProductDto>.Failure($"Product with Code {code} not found")
            : Result<ProductDto>.Success(product);
    }

    public async Task<Result> CreateAsync(Product product)
    {
        var sql = ProductSqlScripts.CreateProduct;

        await _connection.ExecuteAsync(sql, new
        {
            product.Code,
            product.Name,
            product.Price
        });

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Product product)
    {
        var builder = new SqlBuilder();
        var parameters = new DynamicParameters();

        parameters.Add("Id", product.Id);

        if (!string.IsNullOrEmpty(product.Code.ToString()))
        {
            builder.Set("\"Code\" = @Code", new { product.Code });
            parameters.Add("Code", product.Code);
        }

        if (!string.IsNullOrEmpty(product.Name))
        {
            builder.Set("\"Name\" = @Name", new { product.Name });
            parameters.Add("Name", product.Name);
        }

        if (product.Price > 0)
        {
            builder.Set("\"Price\" = @Price", new { product.Price });
            parameters.Add("Price", product.Price);
        }

        var sql = builder.AddTemplate("""
                                          UPDATE "Products"
                                          /**set**/
                                          WHERE "Id" = @Id
                                      """);

        var affectedRows = await _connection.ExecuteAsync(sql.RawSql, parameters);

        return affectedRows == 0 ? Result.Failure("No product found with the specified ID.") : Result.Success();
    }
}