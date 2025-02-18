namespace OnlineMarket.Application.DTOs.OrderProductDto;

public class OrderProductDto
{
    public int ProductId { get; init; }
    public string ProductName { get; init; }
    public int ProductAmount { get; init; }

    public OrderProductDto() { }

    public OrderProductDto(int productId, string productName, int productAmount)
    {
        ProductId = productId;
        ProductName = productName;
        ProductAmount = productAmount;
    }
}