using System.Threading.Tasks;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Orders.GetUserOrder;

namespace EndPoint.Site.Controllers;

[Authorize]
public class OrderController(
    IGetUserOrderService getUserOrderService
) : Controller
{
    private readonly IGetUserOrderService _getUserOrderService = getUserOrderService;

    public async Task<IActionResult> Index()
    {
        var userId = ClaimUtility.GetUserId(User).Value;
        var getResult = await _getUserOrderService.ExecuteAsync(userId);
        return View(getResult.Data);
    }
}