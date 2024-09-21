using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatterns.Product;
using Store.Application.Services.Products.Queries.FetProductForSite;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductFacad _productFacad;
        public ProductsController(IProductFacad ProductFacad)
        {
            _productFacad = ProductFacad;
        }
        public IActionResult Index(Ordering ordering, string SearchKey,long? CatId=null,int page=1,int pageSize=20)
        {
            return View(_productFacad.GetProductForSiteService.Execute(ordering,SearchKey,CatId,page,pageSize).Data);
        }
        public IActionResult Detail(long Id, bool IsPopular)
        {
            return View(_productFacad.GetProductDetailForSiteService.Execute(Id,IsPopular).Data);
        }
    }
}
