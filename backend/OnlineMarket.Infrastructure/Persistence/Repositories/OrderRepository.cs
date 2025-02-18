using OnlineMarket.Application.Interfaces;
using OnlineMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Dapper;
using OnlineMarket.Application.DTOs.OrderDtos;
using OnlineMarket.Application.DTOs.OrderProductDto;
using OnlineMarket.Infrastructure.Data;
using OnlineMarket.Domain;
using OnlineMarket.Infrastructure.SqlScripts;

namespace OnlineMarket.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OnlineMarketDbContext _context;
    private readonly IDbConnection _connection;
    private readonly IUnitOfWork _unitOfWork;

    public OrderRepository(OnlineMarketDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _connection = _context.Database.GetDbConnection();
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetAllAsync()
    {
        var sql = OrderSqlScripts.GetAllOrders;

        var orderDictionary = new Dictionary<int, OrderDto>();

        var orders = await _connection.QueryAsync<OrderDto, OrderProductDto, OrderDto>(
            sql,
            (order, product) =>
            {
                if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
                {
                    currentOrder = new OrderDto(
                        order.Id,
                        order.CreatedAt,
                        order.CustomerFullName,
                        order.CustomerPhone,
                        order.TotalPrice,
                        []
                    );
                    orderDictionary[order.Id] = currentOrder;
                }

                if (product is not null)
                {
                    currentOrder.Products.Add(product);
                }

                return currentOrder;
            },
            splitOn: "ProductId"
        );

        var result = orderDictionary.Values;

        return result.Count == 0
            ? Result<IEnumerable<OrderDto>>.Failure("No orders found")
            : Result<IEnumerable<OrderDto>>.Success(result);

    }

    public async Task<Result<OrderDto>> GetByIdAsync(int id)
    {
        var sql = OrderSqlScripts.GetOrderById;

        var orderDictionary = new Dictionary<int, OrderDto>();

        var orders = await _connection.QueryAsync<OrderDto, OrderProductDto, OrderDto>(
            sql,
            (order, product) =>
            {
                if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
                {
                    currentOrder = new OrderDto(
                        order.Id,
                        order.CreatedAt,
                        order.CustomerFullName,
                        order.CustomerPhone,
                        order.TotalPrice,
                        []
                    );
                    orderDictionary.Add(order.Id, currentOrder);
                }

                if (product != null)
                {
                    currentOrder.Products.Add(product);
                }

                return currentOrder;
            },
            new { Id = id },
            splitOn: "ProductId"
        );

        var result = orderDictionary.Values.FirstOrDefault();

        return result == null
            ? Result<OrderDto>.Failure("Order not found")
            : Result<OrderDto>.Success(result);

    }

    public async Task<Result> CreateAsync(Order order)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var sql = OrderSqlScripts.CreateOrder;

            var orderId = await _connection.QuerySingleAsync<int>(sql, new
            {
                order.CustomerFullName,
                order.CustomerPhone,
                order.CreatedAt
            });

            var insertOrderProductSql = OrderSqlScripts.AddProductToOrder;

            foreach (var orderProduct in order.OrderProducts)
            {
                var totalPrice = await _connection.QueryFirstOrDefaultAsync<decimal>(
                    OrderSqlScripts.GetTotalPrice,
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