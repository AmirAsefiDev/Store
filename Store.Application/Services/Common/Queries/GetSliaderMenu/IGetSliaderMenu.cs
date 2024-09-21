using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Common.Queries.GetMenuItem;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Common.Queries.GetSliaderMenu
{
    public interface IGetSliaderMenu
    {
        ResultDto<List<SliaderMenuItemDto>> Execute();
    }
    public class GetSliaderMenu : IGetSliaderMenu
    {
        private readonly IDataBaseContext _context;
        public GetSliaderMenu(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<SliaderMenuItemDto>> Execute()
        {
            var category = _context.Categories
                .Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == null)
                .ToList()
                .Select(p => new SliaderMenuItemDto
                {
                    CatId = p.Id,
                    Name = p.Name,
                    Child = p.SubCategories.ToList()
                        .Select(child => new SliaderMenuItemDto
                        {
                            CatId = child.Id,
                            Name = child.Name,
                        }).ToList(),
                }).ToList();
            return new ResultDto<List<SliaderMenuItemDto>>()
            {
                Data = category,
                IsSuccess = true,
            };
        }
    }
    public class SliaderMenuItemDto
    {
        public long CatId { get; set; }
        public string Name { get; set; }
        public List<SliaderMenuItemDto> Child { get; set; }
    }
}
