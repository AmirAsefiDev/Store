using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common.Queries.GetSlider;
using Store.Application.Services.HomePages.AddNewSlider;
using Store.Application.Services.HomePages.DeleteSlider;
using Store.Application.Services.HomePages.EditSlider;
using Store.Common.Dto;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IAddNewSliderService _addNewSliderService;
        private readonly IGetSliderService _getSliderService;
        private readonly IDeleteSliderService _deleteSliderService;
        private readonly IEditSliderService _editSliderService;

        public SlidersController(
            IAddNewSliderService AddNewSliderService,
            IGetSliderService GetSliderService,
            IDeleteSliderService DeleteSliderService,
            IEditSliderService EditSliderService
            )
        {
            _addNewSliderService = AddNewSliderService;
            _getSliderService = GetSliderService;
            _deleteSliderService = DeleteSliderService;
            _editSliderService = EditSliderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_getSliderService.Execute().Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file, string link)
        {
            _addNewSliderService.Execute(file, link);
            return View();
        }

        [HttpPost]
        public IActionResult Delete(long SliderId)
        {
            var res = _deleteSliderService.Execute(SliderId);
            return Json(res);
        }

        [HttpPost]
        public IActionResult Edit(long Id, IFormFile Src, string Link)
        {
            if (Src==null)
            {
                new ResultDto
                {
                    Message = "لطفا جهت .یرایش یک عکس اتخاب کنید.",
                    IsSuccess = false,
                };
            }

            var res = _editSliderService.Execute(Id, Src, Link);
            return Json(res);
        }
    }
}
