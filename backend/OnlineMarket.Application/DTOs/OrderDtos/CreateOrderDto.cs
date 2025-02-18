using System.ComponentModel.DataAnnotations;
using OnlineMarket.Application.DTOs.OrderProductDto;

namespace OnlineMarket.Application.DTOs.OrderDtos;

public record CreateOrderDto(string CustomerFullName, string CustomerPhone, List<CreateOrderProductDto> OrderProducts);