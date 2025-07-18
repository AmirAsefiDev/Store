using System.Collections.Generic;

namespace Store.Application.Services.Carts;

public class CartDto
{
    //public long? UserId { get; set; }
    //public Guid BrowserId { get; set; }
    public long CartId { get; set; }
    public List<CartItemDto> CartItems { get; set; }
    public int ProductCount { get; set; }
    public long SumAmount { get; set; }
}