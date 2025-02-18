using OnlineMarket.Application.DTOs.OrderProductDto;

public class OrderDto
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public string CustomerFullName { get; init; }
    public string CustomerPhone { get; init; }
    public decimal TotalPrice { get; init; }
    public List<OrderProductDto> Products { get; init; } = new();

    public OrderDto() { }

    public OrderDto(int id, DateTime createdAt, string customerFullName, string customerPhone, decimal totalPrice, List<OrderProductDto> products)
    {
        Id = id;
        CreatedAt = createdAt;
        CustomerFullName = customerFullName;
        CustomerPhone = customerPhone;
        TotalPrice = totalPrice;
        Products = products;
    }
}