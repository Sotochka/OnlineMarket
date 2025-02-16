using Dapper;
using OnlineMarket.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Domain.Entities;
using System.Data;
using OnlineMarket.Application.DTOs;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Domain;

namespace OnlineMarket.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly OnlineShopDbContext _context;
    private readonly IDbConnection _connection;

    public ProductRepository(OnlineShopDbContext context)
    {
        _context = context;
        _connection = _context.Database.GetDbConnection();
    }

    public async Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync()
    {
        const string sql = @"
            SELECT ""Id"", ""Code"", ""Name"", ""Price"" 
            FROM ""Products""";

        var products = await _connection.QueryAsync<ProductDto>(sql);

        return products == null
            ? Result<IEnumerable<ProductDto>>.Failure("No products found")
            : Result<IEnumerable<ProductDto>>.Success(products);
    }

    public async Task<Result<ProductDto>> GetByIdAsync(int id)
    {
        const string sql = @"
            SELECT ""Id"", ""Code"", ""Name"", ""Price"" 
            FROM ""Products"" 
            WHERE ""Id"" = @Id";

        var product = await _connection.QueryFirstOrDefaultAsync<ProductDto>(sql, new { Id = id });

        return product == null
            ? Result<ProductDto>.Failure($"Product with Id {id} not found")
            : Result<ProductDto>.Success(product);
    }

    public async Task<Result<ProductDto>> GetByCodeAsync(int code)
    {
        const string sql = @"
            SELECT ""Id"", ""Code"", ""Name"", ""Price"" 
            FROM ""Products"" 
            WHERE ""Code"" = @Code";

        var product = await _connection.QueryFirstOrDefaultAsync<ProductDto>(sql, new { Code = code });

        return product == null
            ? Result<ProductDto>.Failure($"Product with Code {code} not found")
            : Result<ProductDto>.Success(product);
    }

    public async Task<Result> CreateAsync(Product product)
    {
        const string sql = @"
            INSERT INTO ""Products"" (""Code"", ""Name"", ""Price"")
            VALUES (@Code, @Name, @Price)";

        await _connection.ExecuteAsync(sql, new
        {
            product.Code,
            product.Name,
            product.Price
        });

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(UpdateProductDto product)
    {
        var setClause = new List<string>();
        var parameters = new DynamicParameters();
        parameters.Add("Id", product.Id);

        if (!string.IsNullOrEmpty(product.Code.ToString()))
        {
            setClause.Add("\"Code\" = @Code");
            parameters.Add("Code", product.Code);
        }

        if (!string.IsNullOrEmpty(product.Name))
        {
            setClause.Add("\"Name\" = @Name");
            parameters.Add("Name", product.Name);
        }

        if (product.Price != 0)
        {
            setClause.Add("\"Price\" = @Price");
            parameters.Add("Price", product.Price);
        }

        if (setClause.Count == 0)
        {
                throw new ArgumentException("At least one field must be updated.");
        }

        var sql = $"""
                               UPDATE "Products" 
                               SET {string.Join(", ", setClause)}
                               WHERE "Id" = @Id
                   """;

        await _connection.ExecuteAsync(sql, parameters);

        return Result.Success();
    }
}