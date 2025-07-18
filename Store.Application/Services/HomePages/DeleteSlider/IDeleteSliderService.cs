using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePages.DeleteSlider
{
    public interface IDeleteSliderService
    {
        ResultDto Execute(long SliderId);
    }
    public class DeleteSliderService : IDeleteSliderService
    {
        private readonly IDataBaseContext _context;
        public DeleteSliderService(IDataBaseContext context)
        {
           _context = context;
        }
        public ResultDto Execute(long SliderId)
        {
            var slider = _context.Sliders.FirstOrDefault(p => p.Id == SliderId);
            if (slider == null)
            {
                new ResultDto{
                    Message = "اسلاید مورد نظر شما برای حذف پیدا نشد",
                    IsSuccess = false,
                };
            }
            slider.RemoveTime = DateTime.Now;
            slider.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "اسلاید شما با موفقیت حذف شد",
            };
        }
    }
}
