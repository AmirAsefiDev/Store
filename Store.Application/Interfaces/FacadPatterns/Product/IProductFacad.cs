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

namespace Store.Application.Interfaces.FacadPatterns.Product
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
        IGetProductForAdminService GetProductForAdminService { get; }

        /// <summary>
        /// دریافت اطلاعات محصول طبق شناسه آن
        /// </summary>
        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }

        /// <summary>
        /// ویرایش اطلاعات محصول
        /// </summary>
        IEditProductService EditProductService { get; }

        /// <summary>
        ///  حذف محصول مورد نظر
        /// </summary>
        IDeleteProductService DeleteProductService { get; }

        /// <summary>
        /// نمایش لیست محصولات در سایت
        /// </summary>
        IGetProductForSiteService GetProductForSiteService { get; }

        /// <summary>
        /// نمایش اطلاعات محصول با شناسه محصول
        /// </summary>
        IGetProductDetailForSiteService GetProductDetailForSiteService { get; }

    }
}
