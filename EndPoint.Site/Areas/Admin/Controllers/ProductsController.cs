using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadPatterns.Product;
using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.Application.Services.Products.Queries.GetProductForAdmin;
using System.Collections.Generic;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductFacad _productFacad;
        public ProductsController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        public IActionResult Index(int Page = 1,int PageSize = 20)
        {
            return View(_productFacad.GetProductForAdminService.Execute(Page,PageSize).Data);
        }

        public IActionResult Detail(long Id)
        {
            return View(_productFacad.GetProductDetailForAdminService.Execute(Id).Data);
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacad.GetAllCategoriesService.Execute().Data,"Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request,List<AddNewProduct_Features> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.Features = Features;
            return Json(_productFacad.AddNewProductService.Execute(request));
        }

        [HttpPost]
        public IActionResult EditProduct(RequestEditProductDto request)
        {
            return Json(_productFacad.EditProductService.Execute(new RequestEditProductDto
            {
                Id = request.Id,
                Name = request.Name,
                Brand = request.Brand,
                Description = request.Description,
                Price = request.Price,
                Inventory = request.Inventory,
                Category = request.Category,
                Displayed = request.Displayed,
            }));
        }

        [HttpPost]
        public IActionResult DeleteProduct(long productId)
        {
            return Json(_productFacad.DeleteProductService.Execute(productId));
        }
    }
}
