using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Orders.CancelOrder;
using Store.Application.Services.Orders.GetOrdersForAdmin;
using Store.Application.Services.Orders.GetUserOrder;
using Store.Domain.Entities.Orders;

namespace EndPoint.Site.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Operator")]
public class OrderController(
    IGetOrdersForAdminService getOrdersForAdminService,
    IGetUserOrderService getUserOrderService,
    ICancelOrderService cancelOrderService
) : Controller
{
    private readonly ICancelOrderService _cancelOrderService = cancelOrderService;
    private readonly IGetOrdersForAdminService _getOrdersForAdminService = getOrdersForAdminService;
    private readonly IGetUserOrderService _getUserOrderService = getUserOrderService;

    public async Task<IActionResult> Index(OrderState orderState)
    {
        var getResult = await _getOrdersForAdminService.ExecuteAsync(orderState);
        return View(getResult.Data);
    }

    public async Task<IActionResult> CancelOrder(long orderId)
    {
        var cancelResult = await _cancelOrderService.ExecuteAsync(orderId);
        return cancelResult ? Ok() : NoContent();
    }
}