namespace OnlineMarket.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public List<OrderProduct> OrderProducts { get; set; }
}