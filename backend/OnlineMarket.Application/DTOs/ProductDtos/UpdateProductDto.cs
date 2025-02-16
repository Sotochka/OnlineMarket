namespace OnlineMarket.Application.DTOs;

public record UpdateProductDto(int Id, int? Code, string? Name, decimal? Price);