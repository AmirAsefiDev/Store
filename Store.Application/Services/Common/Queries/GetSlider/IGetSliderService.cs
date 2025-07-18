using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Common.Queries.GetSlider
{
    public interface IGetSliderService
    {
        ResultDto<List<SliderDto>> Execute();
    }
    public class GetSliderService : IGetSliderService
    {
        private readonly IDataBaseContext _context;
        public GetSliderService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<SliderDto>> Execute()
        {
            //var sliders = _context.Sliders.OrderByDescending(p => p.Id).ToList()
            var sliders = _context.Sliders
                .Select(p=>new SliderDto
                {
                    Id = p.Id,
                    Link = p.Link,
                    Src = p.Src,
                }).ToList();
            return new ResultDto<List<SliderDto>>()
            {
                Data = sliders,
                IsSuccess = true,
            };
        }
    }

    public class SliderDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
    }
}
