using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Queries.GetProductDetailForSite
{
    public interface IGetProductDetailForSiteService
    {
        ResultDto<ProductDetailForSiteDto> Execute(long Id,bool IsPopular);
    }
    public class GetProductDetailForSiteService : IGetProductDetailForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ProductDetailForSiteDto> Execute(long Id,bool IsPopular)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p=>p.ProductImage)
                .Include(p=>p.ProductFeatures)
                .Where(p => p.Id == Id).FirstOrDefault();
            if(product == null)
            {
                throw new Exception("Product Not Found.....");
            }
            if (IsPopular)
            {  
                product.Popularity++;
            }

            product.ViewCount++;
            _context.SaveChanges();
            return new ResultDto<ProductDetailForSiteDto>
            {
                Data = new ProductDetailForSiteDto
                {
                    Brand = product.Brand,
                    Category = $"{product.Category.ParentCategory.Name} - {product.Category.Name}",
                    Description = product.Description,
                    Id = product.Id,
                    Price = product.Price,
                    Title = product.Name,
                    Images = product.ProductImage.Select(p => p.Src).ToList(),
                    Features = product.ProductFeatures.Select(p => new ProductDetailForSite_FeaturesDto
                    {
                        DisplayName = p.DisplayName,
                        Value = p.Value
                    }).ToList(),
                },
                IsSuccess = true,
            };
        }
    }
 
    public class ProductDetailForSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<string> Images { get; set; }
        public List<ProductDetailForSite_FeaturesDto> Features { get; set; }
    }
    public class ProductDetailForSite_FeaturesDto
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
