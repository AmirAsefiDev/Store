using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Finances;
using Store.Domain.Entities.HomePages;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;

namespace Store.Application.Interfaces.Contexts;

public interface IDataBaseContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserInRole> UserInRoles { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductImage> ProductImages { get; set; }
    DbSet<ProductFeatures> ProductFeatures { get; set; }
    DbSet<Slider> Sliders { get; set; }
    DbSet<HomePageImage> HomePageImages { get; set; }
    DbSet<Cart> Carts { get; set; }
    DbSet<CartItem> CartItems { get; set; }

    DbSet<RequestPay> RequestPays { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderDetail> OrderDetails { get; set; }


    int SaveChanges(bool acceptAllChangesOnSuccess);
    int SaveChanges();
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationChangeToken = new());
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}