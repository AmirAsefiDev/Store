using System.Collections.Generic;
using Store.Application.Services.Common.Queries.GetSlider;
using Store.Application.Services.HomePages.GetHomePageImages;
using Store.Application.Services.Products.Queries.FetProductForSite;

namespace EndPoint.Site.Models.ViewModels.HomePages
{
    public class HomePageViewModel
    {
        public List<SliderDto> Sliders { get; set; }
        public List<HomePageImagesDto> PageImages { get; set; }
        public List<ProductForSiteDto> Cameras { get; set; }
        public List<ProductForSiteDto> Mobiles { get; set; }
        public List<ProductForSiteDto> HouseholdAppliances { get; set; }
        public List<ProductForSiteDto> Laptops { get; set; }
    }
}