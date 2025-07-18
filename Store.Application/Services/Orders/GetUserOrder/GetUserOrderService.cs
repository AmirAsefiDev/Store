using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Orders.GetUserOrder;

public class GetUserOrderService(IDataBaseContext context) : IGetUserOrderService
{
    private readonly IDataBaseContext _context = context;

    public async Task<ResultDto<List<GetUserOrderDto>>> ExecuteAsync(long userId)
    {
        var orders = await _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.Id)
            .Select(x => new GetUserOrderDto
            {
                OrderId = x.Id,
                OrderState = x.OrderState,
                RequestPayId = x.RequestPayId,
                OrderDetails = x.OrderDetails.Select(od => new OrderDetailDto
                {
                    OrderDetailId = od.Id,
                    Price = od.Price,
                    Count = od.Count,
                    ProductId = od.ProductId,
                    ProductName = od.Product.Name
                }).ToList()
            })
            .ToListAsync();

        return new ResultDto<List<GetUserOrderDto>>
        {
            Data = orders,
            IsSuccess = true
        };
    }
}