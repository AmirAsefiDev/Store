using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.AddHomePageImages;
using Store.Domain.Entities.HomePages;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageImagesController : Controller
    {
        private readonly IAddHomePageImagesService _addHomePageImagesService;
        public HomePageImagesController(IAddHomePageImagesService addHomePageImagesService)
        {
            _addHomePageImagesService = addHomePageImagesService;
        }
        // [HttpGet]
        // public IActionResult Index()
        // {
        //     return View();
        // }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(IFormFile file,string link,ImageLocation imageLocation)
        {
            _addHomePageImagesService.Execute(new requestAddHomePageImagesDto
            {
                File = file,
                Link = link,
                ImageLocation = imageLocation
            });
            return View();
        }
    }
}
