using OnlineMarket.Domain;
using OnlineMarket.Domain.Entities;

namespace OnlineMarket.Application.Interfaces;

public interface IOrderRepository
{
    Task<Result<IEnumerable<OrderDto>>> GetAllAsync();
    Task<Result<OrderDto>> GetByIdAsync(int id);
    Task<Result> CreateAsync(Order order);
}