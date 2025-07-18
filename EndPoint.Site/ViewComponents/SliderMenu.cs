using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common.Queries.GetSliderMenu;

namespace EndPoint.Site.ViewComponents
{
    public class SliderMenu : ViewComponent
    {
        private readonly IGetSliderMenu _getSliaderMenuItemService;
        public SliderMenu(IGetSliderMenu GetSliaderMenuItemService)
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
