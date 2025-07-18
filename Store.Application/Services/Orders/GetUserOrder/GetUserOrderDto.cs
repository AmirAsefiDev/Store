using System.Collections.Generic;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.GetUserOrder;

public class GetUserOrderDto
{
    public long OrderId { get; set; }
    public OrderState OrderState { get; set; }
    public long RequestPayId { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; } = new();
}