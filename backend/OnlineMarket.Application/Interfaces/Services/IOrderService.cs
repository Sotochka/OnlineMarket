using OnlineMarket.Application.DTOs.OrderDtos;
using OnlineMarket.Domain;

namespace OnlineMarket.Application.Interfaces.Services;

public interface IOrderService
{
    Task<Result<IEnumerable<OrderDto>>> GetOrdersAsync();
    Task<Result<OrderDto>> GetOrderByIdAsync(int id);
    Task<Result> CreateOrderAsync(CreateOrderDto orderDto);
}