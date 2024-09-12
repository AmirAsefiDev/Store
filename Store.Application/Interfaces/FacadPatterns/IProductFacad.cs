using Store.Application.Services.Products.Commands;
using Store.Application.Services.Products.Commands.AddNewCategory;
using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Commands.DeleteParentCategory;
using Store.Application.Services.Products.Commands.EditParentCategory;
using Store.Application.Services.Products.Oueries.GetCategories;
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
       
    }
}
