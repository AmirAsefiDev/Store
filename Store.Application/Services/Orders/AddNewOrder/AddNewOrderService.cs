using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.AddNewOrder;

public class AddNewOrderService(IDataBaseContext context) : IAddNewOrderService
{
    private readonly IDataBaseContext _context = context;

    public async Task<ResultDto> ExecuteAsync(RequestAddNewOrder request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        var requestPay = await _context.RequestPays.FirstOrDefaultAsync(r => r.Id == request.RequestPayId);
        var cart = await _context.Carts
            .Include(p => p.CartItems)
            .ThenInclude(c => c.Product)
            .Where(p => p.Id == request.CartId).FirstOrDefaultAsync();

        requestPay.IsPay = true;
        requestPay.PayDate = DateTime.Now;
        requestPay.Authority = request.Authority;
        requestPay.RefId = request.RefId;

        cart.Finished = true;

        var order = new Order
        {
            Address = "",
            OrderState = OrderState.Processing,
            RequestPay = requestPay,
            User = user
        };
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        List<OrderDetail> orderDetails = new();
        foreach (var item in cart.CartItems)
        {
            var orderDetail = new OrderDetail
            {
                Count = item.Count,
                Order = order,
                Price = item.Product.Price,
                Product = item.Product
            };
            orderDetails.Add(orderDetail);
        }

        await _context.OrderDetails.AddRangeAsync(orderDetails);
        await _context.SaveChangesAsync();

        return new ResultDto
        {
            IsSuccess = true,
            Message = "سفارش و پرداخت شما با موفقیت انجام شد"
        };
    }
}