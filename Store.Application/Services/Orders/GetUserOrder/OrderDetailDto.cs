namespace Store.Application.Services.Orders.GetUserOrder;

public class OrderDetailDto
{
    public long OrderDetailId { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }
}