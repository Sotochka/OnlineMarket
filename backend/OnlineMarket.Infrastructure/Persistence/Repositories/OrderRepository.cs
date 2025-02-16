using OnlineMarket.Application.Interfaces;
using OnlineMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Dapper;
using OnlineMarket.Application.DTOs.OrderDtos;
using OnlineMarket.Application.DTOs.OrderProductDto;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Domain;

namespace OnlineMarket.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OnlineShopDbContext _context;
    private readonly IDbConnection _connection;
    private readonly IUnitOfWork _unitOfWork;

    public OrderRepository(OnlineShopDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _connection = _context.Database.GetDbConnection();
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetAllAsync()
    {
        const string sql = """
                               SELECT o."Id", o."UserId", o."CreatedAt", o."CustomerFullName", o."CustomerPhone",
                                      SUM(op."Amount" * p."Price") OVER (PARTITION BY o."Id") as "TotalPrice",
                                      op."ProductId", p."Name" as "ProductName", op."Amount" as "ProductAmount"
                               FROM "Orders" o
                               LEFT JOIN "OrderProducts" op ON o."Id" = op."OrderId"
                               LEFT JOIN "Products" p ON op."ProductId" = p."Id"
                               ORDER BY o."Id"
                           """;

        var orders = await _connection.QueryAsync<(int Id, int UserId, DateTime CreatedAt, string CustomerFullName, string CustomerPhone, decimal TotalPrice), (int ProductId, string ProductName, int ProductAmount), OrderDto>(
            sql,
            (orderData, productData) => new OrderDto(
                orderData.Id,
                orderData.UserId,
                orderData.CreatedAt,
                orderData.CustomerFullName,
                orderData.CustomerPhone,
                orderData.TotalPrice,
                [new OrderProductDto(productData.ProductId, productData.ProductName, productData.ProductAmount)]
            ),
            splitOn: "ProductId"
        );

        var result = orders
            .GroupBy(o => o.Id)
            .Select(g => g.First() with { Products = g.SelectMany(o => o.Products).ToList() });

        return result == null ? Result<IEnumerable<OrderDto>>.Failure("No orders found")
            : Result<IEnumerable<OrderDto>>.Success(result);
    }

    public async Task<Result<OrderDto>> GetByIdAsync(int id)
    {
        const string sql = """
            SELECT o."Id", o."UserId", o."CreatedAt", o."CustomerFullName", o."CustomerPhone",
                   SUM(op."Amount" * p."Price") OVER (PARTITION BY o."Id") as "TotalPrice",
                   op."ProductId", p."Name" as "ProductName", op."Amount" as "ProductAmount"
            FROM "Orders" o
            LEFT JOIN "OrderProducts" op ON o."Id" = op."OrderId"
            LEFT JOIN "Products" p ON op."ProductId" = p."Id"
            WHERE o."Id" = @Id
            ORDER BY o."Id"
        """;

        var orders = await _connection.QueryAsync<(int Id, int UserId, DateTime CreatedAt, string CustomerFullName, 
            string CustomerPhone, decimal TotalPrice), (int ProductId, string ProductName, int ProductAmount), OrderDto>(
            sql,
            (orderData, productData) => new OrderDto(
                orderData.Id,
                orderData.UserId,
                orderData.CreatedAt,
                orderData.CustomerFullName,
                orderData.CustomerPhone,
                orderData.TotalPrice,
                [new OrderProductDto(productData.ProductId, productData.ProductName, productData.ProductAmount)]
            ),
            new { Id = id },
            splitOn: "ProductId"
        );

        var result = orders
            .GroupBy(o => o.Id)
            .Select(g => g.First() with { Products = g.SelectMany(o => o.Products).ToList() })
            .FirstOrDefault();

        return result == null ? Result<OrderDto>.Failure("Order not found")
            : Result<OrderDto>.Success(result);
    }

    public async Task<Result> CreateAsync(CreateOrderDto orderDto)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            const string insertOrderSql = @"
            INSERT INTO ""Orders"" (""UserId"", ""CustomerFullName"", ""CustomerPhone"", ""CreatedAt"")
            VALUES (@UserId, @CustomerFullName, @CustomerPhone, @CreatedAt)
            RETURNING ""Id""";

            var orderId = await _connection.QuerySingleAsync<int>(insertOrderSql, new
            {
                orderDto.UserId,
                orderDto.CustomerFullName,
                orderDto.CustomerPhone,
                CreatedAt = DateTime.Now
            });

            const string insertOrderProductSql = @"
            INSERT INTO ""OrderProducts"" (""OrderId"", ""ProductId"", ""Amount"", ""TotalPrice"")
            VALUES (@OrderId, @ProductId, @Amount, @TotalPrice)";

            foreach (var orderProduct in orderDto.OrderProducts)
            {
                var totalPrice = await _connection.QueryFirstOrDefaultAsync<decimal>(
                    @"SELECT p.""Price"" * @Amount as TotalPrice 
                FROM ""Products"" p 
                WHERE p.""Id"" = @ProductId",
                    new { orderProduct.ProductId, orderProduct.Amount }
                );

                if (totalPrice == 0)
                {
                    throw new Exception($"Product with id {orderProduct.ProductId} not found");
                }

                await _connection.ExecuteAsync(insertOrderProductSql, new
                {
                    OrderId = orderId,
                    orderProduct.ProductId,
                    orderProduct.Amount,
                    TotalPrice = totalPrice
                });
            }
            await _unitOfWork.CommitTransactionAsync();
            return Result.Success();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new Exception(e.Message);
        }
    }
}