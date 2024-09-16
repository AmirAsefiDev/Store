using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.EditProduct
{
    public interface IEditProductService
    {
        ResultDto Execute(RequestEditProductDto request);
    }
    public class EditProductService : IEditProductService
    {
        private readonly IDataBaseContext _context;
        public EditProductService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestEditProductDto request)
        {
            var product = _context.Products.Find(request.Id);
            var category = _context.Categories.FirstOrDefault(c=> c.Name == request.Category);
            if (product == null && category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول مد نظر شما یافت نشد"
                };
            };
            product.Name = request.Name;
            product.Brand = request.Brand;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Inventory = request.Inventory;
            product.CategoryId = category.Id;
            product.Displayed = request.Displayed;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویرایش محصول انجام شد"
            };
        }
    }

    public class RequestEditProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public string Category { get; set; }
        public long CategoryId { get; set; }
        public bool Displayed { get; set; }
        //public List<IFormFile> Images { get; set; }
        //public List<EditProduct_Features> Features { get; set; }
    }
    public class EditProduct_Features
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }

}
