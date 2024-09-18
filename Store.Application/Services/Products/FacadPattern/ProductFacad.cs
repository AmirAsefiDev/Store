using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPatterns.Product;
using Store.Application.Services.Products.Commands.AddNewCategory;
using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Commands.DeleteParentCategory;
using Store.Application.Services.Products.Commands.DeleteProduct;
using Store.Application.Services.Products.Commands.EditParentCategory;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.Application.Services.Products.Queries.FetProductForSite;
using Store.Application.Services.Products.Queries.GetAllCategories;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Application.Services.Products.Queries.GetProductDetailForSite;
using Store.Application.Services.Products.Queries.GetProductForAdmin;
using Store.Application.Services.Products.Queries.GetProructDetailForAdmin;
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
        private readonly IHostingEnvironment _environment;
        public ProductFacad(IDataBaseContext context, IHostingEnvironment Environment)
        {
            _context = context;
            _environment = Environment;
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
        private AddNewProductService _addNewProductService;
        public AddNewProductService AddNewProductService
        {
            get
            {
                return _addNewProductService = _addNewProductService ?? new AddNewProductService(_context,_environment);
            }
        }

        private IGetAllCategoriesService _getAllCategoriesService;
        public IGetAllCategoriesService GetAllCategoriesService
        {
            get
            {
                return _getAllCategoriesService = _getAllCategoriesService ?? new GetAllCategoriesService(_context);
            }
        }
        private IGetProductForAdminService _getProductForAdminService;
        public IGetProductForAdminService GetProductForAdminService
        {
            get
            {
                return _getProductForAdminService = _getProductForAdminService ?? new GetProductForAdminService(_context);
            }
        }
        private IGetProductDetailForAdminService _getProductDetailForAdminService;
        public IGetProductDetailForAdminService GetProductDetailForAdminService
        {
            get
            {
                return _getProductDetailForAdminService = _getProductDetailForAdminService ?? new GetProductDetailForAdminService(_context);
            }
        }
        private IEditProductService _editProductService;
        public IEditProductService EditProductService
        {
            get
            {
                return _editProductService = _editProductService ?? new EditProductService(_context);
            }
        }
        private IDeleteProductService _deleteProductService;
        public IDeleteProductService DeleteProductService
        {
            get
            {
                return _deleteProductService = _deleteProductService ?? new DeleteProductService(_context);
            }
        }
        private IGetFetProductForSiteService _getFetProductForSiteService;
        public IGetFetProductForSiteService GetProductForSiteService 
        {
            get
            {
                return _getFetProductForSiteService = _getFetProductForSiteService ?? new GetFetProductForSiteService(_context); 
            }
        }
        private IGetProductDetailForSiteService _getProductDetailForSiteService;
        public IGetProductDetailForSiteService GetProductDetailForSiteService
        {
            get
            {
                return _getProductDetailForSiteService = _getProductDetailForSiteService ?? new GetProductDetailForSiteService(_context);
            }
        }
    }
}
