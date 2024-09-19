using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Queries.FetProductForSite
{
    public interface IGetFetProductForSiteService
    {
        public ResultDto<ResultProductForSiteDto>Execute(string SearchKey,int Page,long? categoryId);
    }
    public class GetFetProductForSiteService : IGetFetProductForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetFetProductForSiteService(IDataBaseContext context)
        {
            _context = context;
        }  
        public ResultDto<ResultProductForSiteDto> Execute(string SearchKey, int Page, long? CatId)
        {
            int totalRow = 0;
            var productQuery = _context.Products
                .Include(p => p.ProductImage).AsQueryable();

            if(CatId != null)
            {
                productQuery = productQuery.
                    Where(p => p.CategoryId == CatId || p.Category.ParentCategoryId == CatId).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(SearchKey))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(SearchKey) || p.Brand.Contains(SearchKey)).AsQueryable();
            }
                var product = productQuery.ToPaged(Page, 5 ,out totalRow);
            Random random = new Random();


            return new ResultDto<ResultProductForSiteDto>
            {
                Data = new ResultProductForSiteDto
                {
                    TotalRow = totalRow,
                    Products = product.Select(p => new ProductForSiteDto
                    {
                        Id = p.Id,
                        Star = random.Next(1, 5),
                        Title = p.Name,
                        Price = p.Price,
                        ImageSrc = p.ProductImage.FirstOrDefault().Src,
                    }).ToList(),
                },
                IsSuccess = true,
            };
        }
    }

    public class ResultProductForSiteDto
    {
        public List<ProductForSiteDto> Products { get; set; }
        public int TotalRow { get; set; }
    }
    public class ProductForSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string ImageSrc { get; set; }
        public int Star { get; set; }
    }
}
