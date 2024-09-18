using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common.Queries.GetMenuItem;

namespace EndPoint.Site.ViewComponents
{
    public class GetMenu : ViewComponent
    {
        private readonly IGetMenuItemService _getMenuItemService;
        public GetMenu(IGetMenuItemService GetMenuItemService)
        {
            _getMenuItemService = GetMenuItemService;
        }

        public IViewComponentResult Invoke()
        {
            var menuItem = _getMenuItemService.Execute();
            return View(viewName: "GetMenu", menuItem.Data);
        }
    }
}
