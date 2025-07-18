using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.GetOrdersForAdmin;

public class GetOrdersForAdminService(
    IDataBaseContext context
) : IGetOrdersForAdminService
{
    private readonly IDataBaseContext _context = context;

    public async Task<ResultDto<List<OrdersDto>>> ExecuteAsync(OrderState orderState)
    {
        var orders = await _context.Orders.Include(o => o.OrderDetails)
            .Where(o => o.OrderState == orderState)
            .OrderByDescending(o => o.Id)
            .Select(o => new OrdersDto
            {
                OrderId = o.Id,
                OrderState = o.OrderState,
                UserId = o.UserId,
                RequestPayId = o.RequestPayId,
                ProductCount = o.OrderDetails.Count(),
                InsertTime = o.InsertTime
            }).ToListAsync();
        return new ResultDto<List<OrdersDto>>
        {
            Data = orders,
            IsSuccess = true
        };
    }
}