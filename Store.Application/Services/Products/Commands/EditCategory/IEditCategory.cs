using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.EditParentCategory
{
    public interface IEditCategory
    {
        ResultDto Execute(RequestEditCategoryDto request);
    }
    public class EditCategory : IEditCategory
    {
        private readonly IDataBaseContext _context;
        public EditCategory(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestEditCategoryDto request)
        {
            var category = _context.Categories.Find(request.Id);
            if(category == null)
            {
                return new ResultDto 
                { 
                    IsSuccess = false,
                    Message = "دسته بندی  مد نظر شما یافت نشد"
                };
            };
            category.Name = request.Name;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویرایش دسته بندی انجام شد"
            };
        }
    }

    

    public class RequestEditCategoryDto 
    {
        public long Id { get; set; }    
        public string Name { get; set; }
    }
}
