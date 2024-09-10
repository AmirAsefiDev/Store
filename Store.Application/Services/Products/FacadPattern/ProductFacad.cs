using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Products.Commands.AddNewCategory;
using Store.Application.Services.Products.Commands.DeleteParentCategory;
using Store.Application.Services.Products.Commands.EditParentCategory;
using Store.Application.Services.Products.Oueries.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext _context;
        public ProductFacad(IDataBaseContext context)
        {
            _context = context;
        }
        private AddNewCategoryService _addNewCategory;
        public AddNewCategoryService AddNewCategoryService
        {
            get
            {
                return _addNewCategory = _addNewCategory ?? new AddNewCategoryService(_context);
            }
        }

        private IGetCategoryService _getCategoryService;
        public IGetCategoryService GetCategoryService
        {
            get
            {
                return _getCategoryService = _getCategoryService ?? new GetCategoryService(_context);
            }
        }
        private IDeleteCategory _deleteCategory;


        public IDeleteCategory DeleteCategory
        {
            get
            {
                return _deleteCategory = _deleteCategory ?? new DeleteCategory(_context);
            }
        }

        private IEditCategory _editCategory;
        public IEditCategory EditCategory
        {
            get
            {
                return  _editCategory = _editCategory ?? new EditCategory(_context);
            }
        }
    }
}
