using System.Threading.Tasks;

namespace Store.Application.Services.Orders.CancelOrder;

public interface ICancelOrderService
{
    Task<bool> ExecuteAsync(long orderId);
}