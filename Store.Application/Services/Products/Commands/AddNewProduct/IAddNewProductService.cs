using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.AddNewProduct
{
    public interface IAddNewProductService
    {
        ResultDto Execute(RequestAddNewProductDto request);
    }

    public class AddNewProductService : IAddNewProductService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddNewProductService(IDataBaseContext context, IHostingEnvironment Environment)
        {
            _context = context;
            _environment = Environment;
        }
        public ResultDto Execute(RequestAddNewProductDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Brand))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message =  "نام برند محصول را وارد کنید"
                };
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message =  "نام محصول را وارد کنید"
                };
            }
            if (string.IsNullOrWhiteSpace(request.Price.ToString()))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message =  "قیمت محصول را وارد کنید"
                };
            }
            if (string.IsNullOrWhiteSpace(request.Inventory.ToString()))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message =  "تعداد محصول را وارد کنید"
                };
            }
            if (string.IsNullOrWhiteSpace(request.CategoryId.ToString()))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message =  "دسته بندی محصول را وارد کنید"
                };
            }

            try
            {
                var category = _context.Categories.Find(request.CategoryId);
                Product product = new Product()
                {
                    Brand = request.Brand,
                    Description = request.Description,
                    Name = request.Name,
                    Price = request.Price,
                    Inventory = request.Inventory,
                    Category = category,
                    Displayed = request.Displayed,
                };
                _context.Products.Add(product); 

                List<ProductImage> productImages = new List<ProductImage>();
                foreach (var item in request.Images)
                {
                    var uploadedResult = UploadFile(item);
                    productImages.Add(new ProductImage()
                    {
                        Product = product,
                        Src = uploadedResult.FileNameAddress,
                    });
                }
                _context.ProductImages.AddRange(productImages);

                List<ProductFeatures> productFeatures = new List<ProductFeatures>();
                foreach(var item in request.Features)
                {
                    productFeatures.Add(new ProductFeatures
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product,
                    });
                }
                _context.ProductFeatures.AddRange(productFeatures);

                _context.SaveChanges();
                return new ResultDto{
                    IsSuccess = true,
                    Message = "محصول با موفقیت به محصولات فروشگاه اضافه شد",
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطا رخ داد ",
                };
            };
            

        }
        public UploadDto UploadFile(IFormFile file)
        {    
            if(file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadRootFolder = Path.Combine(_environment.WebRootPath,folder);
                if (!Directory.Exists(uploadRootFolder))
                {
                    Directory.CreateDirectory(uploadRootFolder);
                }
                if(file==null || file.Length == 0)
                {
                    return new UploadDto() 
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }
                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }
        public class UploadDto
        {
            public long Id { get; set; }
            public bool Status { get; set; }
            public string FileNameAddress { get; set; }
        }
    }

    public class RequestAddNewProductDto
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public long CategoryId { get; set; } 
        public bool Displayed { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<AddNewProduct_Features> Features { get; set; }
    }
    public class AddNewProduct_Features
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
