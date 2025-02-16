using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.Application.DTOs;

public record UpdateProductDto(
    [Required(ErrorMessage = "Id is required")]
    int Id,

    [Range(1, int.MaxValue, ErrorMessage = "Code must be greater than 0")]
    int? Code,

    [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
    string? Name,

    [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    decimal? Price);