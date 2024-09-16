using Store.Application.Services.Products.Commands;
using Store.Application.Services.Products.Commands.AddNewCategory;
using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Commands.DeleteParentCategory;
using Store.Application.Services.Products.Commands.DeleteProduct;
using Store.Application.Services.Products.Commands.EditParentCategory;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.Application.Services.Products.Queries.GetAllCategories;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Application.Services.Products.Queries.GetProductForAdmin;
using Store.Application.Services.Products.Queries.GetProructDetailForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        AddNewCategoryService AddNewCategoryService { get; }

        IGetCategoryService GetCategoryService { get; }

        IDeleteCategory DeleteCategory { get; }

        IEditCategory EditCategory { get; }
        AddNewProductService AddNewProductService { get; }

        /// <summary>
        /// دریافت لیست محصول
        /// </summary>
        IGetAllCategoriesService GetAllCategoriesService { get; }

        /// <summary>
        /// دریافت لیست محصول برای مدیر سایت
        /// </summary>
        IGetProductForAdminService  GetProductForAdminService { get; }

        /// <summary>
        /// دریافت اطلاعات محصول طبق شناسه آن
        /// </summary>
        IGetProductDetailForAdminService GetProductDetailForAdminService { get;  }

        /// <summary>
        /// ویرایش اطلاعات محصول
        /// </summary>
        IEditProductService EditProductService { get; }

        /// <summary>
        ///  حذف محصول مورد نظر
        /// </summary>
        IDeleteProductService DeleteProductService { get; }

    }
}
