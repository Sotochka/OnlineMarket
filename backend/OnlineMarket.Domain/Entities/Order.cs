namespace OnlineMarket.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CustomerFullName { get; set; }
    public string CustomerPhone { get; set; }

    public List<OrderProduct> OrderProducts { get; set; }
}