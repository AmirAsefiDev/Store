using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Common.Queries.GetMenuItem;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Common.Queries.GetSliderMenu
{
    public interface IGetSliderMenu
    {
        ResultDto<List<SliderMenuItemDto>> Execute();
    }
    public class GetSliderMenu : IGetSliderMenu
    {
        private readonly IDataBaseContext _context;
        public GetSliderMenu(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<SliderMenuItemDto>> Execute()
        {
            var category = _context.Categories
                .Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == null)
                .ToList()
                .Select(p => new SliderMenuItemDto
                {
                    CatId = p.Id,
                    Name = p.Name,
                    Child = p.SubCategories.ToList()
                        .Select(child => new SliderMenuItemDto
                        {
                            CatId = child.Id,
                            Name = child.Name,
                        }).ToList(),
                }).ToList();
            return new ResultDto<List<SliderMenuItemDto>>()
            {
                Data = category,
                IsSuccess = true,
            };
        }
    }
    public class SliderMenuItemDto
    {
        public long CatId { get; set; }
        public string Name { get; set; }
        public List<SliderMenuItemDto> Child { get; set; }
    }
}
