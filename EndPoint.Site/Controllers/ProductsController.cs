using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatterns.Product;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductFacad _productFacad;
        public ProductsController(IProductFacad ProductFacad)
        {
            _productFacad = ProductFacad;
        }
        public IActionResult Index(string SearchKey,int page=1,long? catId=null)
        {
            return View(_productFacad.GetProductForSiteService.Execute(SearchKey,page,catId).Data);
        }
        public IActionResult Detail(long Id)
        {
            return View(_productFacad.GetProductDetailForSiteService.Execute(Id).Data);
        }
    }
}
