using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common.Queries.GetCategory;

namespace EndPoint.Site.ViewComponents
{
    public class Search : ViewComponent
    {
        private readonly IGetCategoryService _getCategoryService;
        public Search(IGetCategoryService GetCategoryService)
        {
            _getCategoryService = GetCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(viewName: "Search", _getCategoryService.Execute().Data);
        }
    }
}
