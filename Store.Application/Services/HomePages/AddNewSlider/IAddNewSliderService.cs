using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;
using System;
using System.IO;
using static Store.Application.Services.Products.Commands.AddNewProduct.AddNewProductService;

namespace Store.Application.Services.HomePages.AddNewSlider
{
    public interface IAddNewSliderService
    {
        ResultDto Execute(IFormFile File, string link);
    }

    public class AddNewSliderService : IAddNewSliderService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddNewSliderService(IDataBaseContext Context, IHostingEnvironment Environment)
        {
            _environment = Environment;
            _context = Context;
        }
        public ResultDto Execute(IFormFile File, string link)
        {
            var resultUpload = UploadFile(File);

            Slider slider = new Slider()
            {
                Link = link,
                Src = resultUpload.FileNameAddress,
            };
            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return new ResultDto
            {
                Message = "عکس شما با موفقیت اضافه شد",
                IsSuccess =  true,
            };
        }
        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\HomePages\Sliders\";
                var uploadRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadRootFolder))
                {
                    Directory.CreateDirectory(uploadRootFolder);
                }
                if (file == null || file.Length == 0)
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
    }


}
