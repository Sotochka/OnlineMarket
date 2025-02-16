namespace OnlineMarket.Application.DTOs.OrderDtos;

public record OrderDto(int Id, int UserId, DateTime CreatedAt, string CustomerFullName, string CustomerPhone, decimal TotalPrice, List<OrderProductDto.OrderProductDto> Products);