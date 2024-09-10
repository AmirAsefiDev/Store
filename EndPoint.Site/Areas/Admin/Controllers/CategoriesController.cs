using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Products.Commands.EditParentCategory;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductFacad _productFacad;
        public CategoriesController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        [HttpGet]
        public IActionResult Index(long? parentId)
        {
            //PartialView("_ChildLinks", parentId);
            return View(_productFacad.GetCategoryService.Execute(parentId).Data);
        }

        [HttpGet]
        public IActionResult AddNewCategory(long? parentId)
        {
            ViewBag.parentId = parentId;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(long? parentId,string Name)
        {
            var result = _productFacad.AddNewCategoryService.Execute(parentId, Name);
            return Json(result); 
        }

        [HttpPost]
        public IActionResult DeleteCategory(long ParentId)
        {
            var result = _productFacad.DeleteCategory.Execute(ParentId);
            return Json(result);
        }

        [HttpPost]
        public IActionResult EditCategory(RequestEditCategoryDto request) 
        {
            return Json(_productFacad.EditCategory.Execute(new RequestEditCategoryDto
            {
                Id = request.Id,
                Name = request.Name,
            }));
        }
    }
}
