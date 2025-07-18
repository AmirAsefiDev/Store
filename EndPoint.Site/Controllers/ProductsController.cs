using System;
using Microsoft.AspNetCore.Http;
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

        public IActionResult Index(Ordering ordering, string SearchKey, long? CatId = null, int page = 1,
            int pageSize = 20)
        {
            Response.Cookies.Append("UserId", "2", new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                Path = Request.PathBase.HasValue ? Request.PathBase.ToString() : "/",
                Expires = DateTime.Now.AddDays(3)
            }); // this way make a new cookie
            return View(_productFacad.GetProductForSiteService.Execute(ordering, SearchKey, CatId, page, pageSize)
                .Data);
        }

        //public IActionResult AccessCookie()
        //{
        //    string cookieValue;
        //    if (Request.Cookies.TryGetValue("UserId", out cookieValue)) return Ok(cookieValue);

        //    return NotFound("Cookie isn't available");
        //    // = Request.Cookies["UserId"];
        //    //if (string.IsNullOrEmpty(cookieValue))
        //    //    return NotFound("Cookie isn't available");
        //}

        //public IActionResult RemoveCookie()
        //{
        //    Response.Cookies.Delete("UserId");
        //    return Ok("UserId Cookie Deleted");
        //}

        public IActionResult Detail(long Id, bool IsPopular)
        {
            var result = _productFacad.GetProductDetailForSiteService.Execute(Id, IsPopular).Data;
            return View(result);
        }
    }
}