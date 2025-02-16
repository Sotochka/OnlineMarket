using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.Application.DTOs;

public record CreateProductDto(
    [Range(1, int.MaxValue, ErrorMessage = "Code must be greater than 0.")]
    int Code,

    [Required(ErrorMessage = "Product name is required.")]
    [MaxLength(100, ErrorMessage = "Product name must not exceed 100 characters.")]
    string Name,

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    decimal Price);