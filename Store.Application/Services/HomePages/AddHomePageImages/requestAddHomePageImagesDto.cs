using Microsoft.AspNetCore.Http;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.AddHomePageImages
{
    public class requestAddHomePageImagesDto
    {
        public IFormFile File { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }  
    }
}