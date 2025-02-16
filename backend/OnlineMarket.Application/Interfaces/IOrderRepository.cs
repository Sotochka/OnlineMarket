using OnlineMarket.Application.DTOs.OrderDtos;
using OnlineMarket.Domain;

namespace OnlineMarket.Application.Interfaces;

public interface IOrderRepository
{
    Task<Result<IEnumerable<OrderDto>>> GetAllAsync();
    Task<Result<OrderDto>> GetByIdAsync(int id);
    Task<Result> CreateAsync(CreateOrderDto orderDto);
}