using System.Diagnostics;
using EndPoint.Site.Models;
using EndPoint.Site.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Application.Interfaces.FacadPatterns.Product;
using Store.Application.Services.Common.Queries.GetSlider;
using Store.Application.Services.HomePages.GetHomePageImages;
using Store.Application.Services.Products.Queries.FetProductForSite;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetHomePageImagesService _getHomePageImagesService;
        private readonly IGetSliderService _getSliderService;
        private readonly ILogger<HomeController> _logger;
        private readonly IProductFacad _productFacad;

        public HomeController(ILogger<HomeController> logger, IGetSliderService GetSliderService,
            IGetHomePageImagesService getHomePageImagesService, IProductFacad productFacad)
        {
            _logger = logger;
            _getSliderService = GetSliderService;
            _getHomePageImagesService = getHomePageImagesService;
            _productFacad = productFacad;
        }

        public IActionResult Index()
        {
            var homePage = new HomePageViewModel
            {
                Sliders = _getSliderService.Execute().Data,
                PageImages = _getHomePageImagesService.Execute().Data,
                Cameras = _productFacad.GetProductForSiteService.Execute(Ordering.theNewest, null, 20, 1, 6)
                    .Data.Products
            };
            return View(homePage);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}