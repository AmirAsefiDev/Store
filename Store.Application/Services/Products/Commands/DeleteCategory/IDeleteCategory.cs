using Microsoft.EntityFrameworkCore.Query;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.DeleteParentCategory
{
    public interface IDeleteCategory
    {
        ResultDto Execute(long ParentId);
    }

    public class DeleteCategory : IDeleteCategory
    {
        private readonly IDataBaseContext _context;

        public DeleteCategory(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long ParentId)
        {
            var category = _context.Categories.Find(ParentId);
            if(category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "دستوری برای حذف پیدا نشد"
                };
            }
            category.RemoveTime = DateTime.Now;
            category.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "دسته بندی مورد نظر شما حذف شد"
            };
        }
    }
}
