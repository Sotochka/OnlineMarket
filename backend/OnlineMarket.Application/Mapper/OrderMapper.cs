using OnlineMarket.Application.DTOs.OrderDtos;
using OnlineMarket.Application.DTOs.OrderProductDto;
using OnlineMarket.Domain.Entities;

namespace OnlineMarket.Application.Mapper;

public static class OrderMapper
{
    public static Order ToEntity(this CreateOrderDto createOrderDto)
    {
        return new Order
        {
            CustomerFullName = createOrderDto.CustomerFullName,
            CustomerPhone = createOrderDto.CustomerPhone,
            CreatedAt = DateTime.Now,
            OrderProducts = createOrderDto.OrderProducts.Select(op => new OrderProduct
            {
                ProductId = op.ProductId,
                Amount = op.Amount
            }).ToList()
        };
    }

    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            CustomerFullName = order.CustomerFullName,
            CustomerPhone = order.CustomerPhone,
            TotalPrice = order.OrderProducts.Sum(op => op.Amount * op.Product.Price),
            Products = order.OrderProducts.Select(op => new OrderProductDto
            {
                ProductId = op.ProductId,
                ProductName = op.Product.Name,
                ProductAmount = op.Amount
            }).ToList()
        };
    }
}