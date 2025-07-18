namespace Store.Application.Services.Carts;

public class CartItemDto
{
    public long Id { get; set; }
    public string Product { get; set; }
    public long ProductId { get; set; }
    public string Images { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}