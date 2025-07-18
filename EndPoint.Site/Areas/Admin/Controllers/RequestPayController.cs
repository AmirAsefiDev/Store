using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Finances.GetRequestPayForAdmin;

namespace EndPoint.Site.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class RequestPayController(IGetRequestPayForAdminService getRequestPayForAdminService) : Controller
{
    private readonly IGetRequestPayForAdminService _getRequestPayForAdminService = getRequestPayForAdminService;

    public async Task<IActionResult> Index()
    {
        var getResult = await _getRequestPayForAdminService.ExecuteAsync();
        return View(getResult.Data);
    }
}