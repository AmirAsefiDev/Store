using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common.Queries.GetSliaderMenu;

namespace EndPoint.Site.ViewComponents
{
    public class SliderMenu : ViewComponent
    {
        private readonly IGetSliaderMenu _getSliaderMenuItemService;
        public SliderMenu(IGetSliaderMenu GetSliaderMenuItemService)
        {
            _getSliaderMenuItemService = GetSliaderMenuItemService;
        }
        public IViewComponentResult Invoke()
        {
            var SliaderMenu = _getSliaderMenuItemService.Execute();
            return View(viewName: "SliderMenu", SliaderMenu.Data);
        }
    }
}
