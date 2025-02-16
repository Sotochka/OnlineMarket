using System.ComponentModel.DataAnnotations;
using OnlineMarket.Application.DTOs.OrderProductDto;

namespace OnlineMarket.Application.DTOs.OrderDtos;

public record CreateOrderDto(
    [Required(ErrorMessage = "User id is required.")]
    int UserId,

    [Required(ErrorMessage = "Customer full name is required.")]
    [MaxLength(100)]
    string CustomerFullName,

    [Required(ErrorMessage = "Customer phone is required.")]
    [RegularExpression(@"^\+?[0-9\s\-\(\)]{7,20}$", ErrorMessage = "Invalid phone number format.")]
    string CustomerPhone,

    [Required(ErrorMessage = "You need to choose a product to order.")]
    List<CreateOrderProductDto> OrderProducts);