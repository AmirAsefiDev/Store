using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.HomePages.GetHomePageImages
{
    public class GetHomePageImagesService(IDataBaseContext context)  : IGetHomePageImagesService
    {
        private readonly IDataBaseContext _context = context;

        public ResultDto<List<HomePageImagesDto>> Execute()
        {
            var images = _context.HomePageImages
                .OrderByDescending(p => p.Id)
                .Select(h => new HomePageImagesDto
                {
                    Id = h.Id,
                    ImageLocation = h.ImageLocation,
                    Link = h.Link,
                    Src = h.Src,
                }).ToList();
            return new ResultDto<List<HomePageImagesDto>>
            {
                Data = images,
                IsSuccess = true,
                Message = "information loaded successfully"
            };

        }
    }
}
