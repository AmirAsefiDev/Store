namespace Store.Application.Services.Orders.AddNewOrder;

public class RequestAddNewOrder
{
    public long CartId { get; set; }
    public long RequestPayId { get; set; }
    public long UserId { get; set; }
    public long RefId { get; set; }
    public string Authority { get; set; }
}