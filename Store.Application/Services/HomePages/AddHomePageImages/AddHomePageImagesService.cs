using GharareSabz.Common;
using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.AddHomePageImages;

public class AddHomePageImagesService : IAddHomePageImagesService
{
    private readonly IDataBaseContext _contnxt;
    private readonly IHostingEnvironment _environment;

    public AddHomePageImagesService(IDataBaseContext contnxt, IHostingEnvironment environment)
    {
        _contnxt = contnxt;
        _environment = environment;
    }

    public ResultDto Execute(requestAddHomePageImagesDto request)
    {
        var uploader = new Uploader(_environment);
        var resUpload = uploader.UploadFile(request.File, @"images\HomePages\Slider\");

        HomePageImage homePageImage = new()
        {
            Link = request.Link,
            Src = resUpload.FileNameAddress,
            ImageLocation = request.ImageLocation
        };
        _contnxt.HomePageImages.Add(homePageImage);
        _contnxt.SaveChanges();

        return new ResultDto
        {
            IsSuccess = true,
            Message = "عکس در صفحه اصلی شما با موفقیت اضافه شد."
        };
    }
}