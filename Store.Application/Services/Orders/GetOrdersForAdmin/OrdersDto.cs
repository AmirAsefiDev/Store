using System;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.GetOrdersForAdmin;

public class OrdersDto
{
    public long OrderId { get; set; }
    public DateTime InsertTime { get; set; }
    public long RequestPayId { get; set; }
    public long UserId { get; set; }
    public OrderState OrderState { get; set; }
    public int ProductCount { get; set; }
}