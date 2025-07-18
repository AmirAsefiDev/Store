using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Store.Application.Services.Products.Commands.AddNewProduct.AddNewProductService;

namespace Store.Application.Services.HomePages.EditSlider
{
    public interface IEditSliderService
    {
        ResultDto Execute(long SliderId, IFormFile File, string link);
    }
    public class EditSliderService : IEditSliderService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;

        public EditSliderService(IDataBaseContext Context,IHostingEnvironment Environment)
        {
            _context = Context;
            _environment = Environment;

        }
        public ResultDto Execute(long SliderId, IFormFile File, string link)
        {
            var resultUplaod = UploadFile(File);

            Slider slider = new()
            {
                Id = SliderId,
                Link = link,
                Src = resultUplaod.FileNameAddress,
            };

            _context.Sliders.Update(slider);
            _context.SaveChanges();

            return new ResultDto 
            { 
                Message = "اسلاید مورد نظر شما با موفقیت ویرایش شد",
                IsSuccess = true,
            };
        }
        private UploadDto UploadFile(IFormFile File)
        {
            if(File != null)
            {
                string folder = $@"images\HomePages\Sliders\";
                var uploadRootFolder = Path.Combine(_environment.WebRootPath,folder);
                if (!Directory.Exists(uploadRootFolder))
                {
                    Directory.CreateDirectory(uploadRootFolder);
                }
                if(File==null || File.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }
                string fileName = DateTime.Now.Ticks.ToString() + File.FileName;
                var filePath = Path.Combine(uploadRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    File.CopyTo(fileStream);
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
