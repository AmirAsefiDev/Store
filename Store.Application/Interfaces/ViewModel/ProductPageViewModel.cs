using Store.Application.Services.Products.Commands.EditProduct;
using Store.Application.Services.Products.Queries.GetProductForAdmin;

namespace EndPoint.Site.Areas.Admin.Views.Products.ViewModel
{
    public class ProductPageViewModel
    {
       public RequestEditProductDto RequestEditProductDto { get ; set; }
       public ProductForAdminDto ProductForAdminDto { get ; set; } 
    }
}
