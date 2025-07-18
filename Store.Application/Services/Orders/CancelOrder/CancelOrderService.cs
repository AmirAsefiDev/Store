using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.CancelOrder;

public class CancelOrderService(IDataBaseContext context) : ICancelOrderService
{
    private readonly IDataBaseContext _context = context;

    public async Task<bool> ExecuteAsync(long orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) return false;

        order.OrderState = OrderState.Canceled;
        await _context.SaveChangesAsync();

        return true;
    }
}