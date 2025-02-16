using OnlineMarket.Application.DTOs.OrderDtos;
using OnlineMarket.Application.Interfaces;
using OnlineMarket.Application.Interfaces.Services;
using OnlineMarket.Domain;

namespace OnlineMarket.Application.Services;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<Result<IEnumerable<OrderDto>>> GetOrdersAsync()
    {
        var orders = await orderRepository.GetAllAsync();

        return orders == null ? Result<IEnumerable<OrderDto>>.Failure("No orders found")
            : Result<IEnumerable<OrderDto>>.Success(orders.Value);
    }

    public async Task<Result<OrderDto>> GetOrderByIdAsync(int id)
    {
        var orders = await orderRepository.GetByIdAsync(id);

        return orders.IsSuccess ? Result<OrderDto>.Success(orders.Value)
            : Result<OrderDto>.Failure(orders.ErrorMessage);
    }

    public async Task<Result> CreateOrderAsync(CreateOrderDto orderDto)
    {
        var result = await orderRepository.CreateAsync(orderDto);

        return result.IsSuccess ? Result.Success()
            : Result.Failure(result.ErrorMessage);
    }
}