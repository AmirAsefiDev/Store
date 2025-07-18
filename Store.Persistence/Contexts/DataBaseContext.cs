using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Roles;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Finances;
using Store.Domain.Entities.HomePages;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;

namespace Store.Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HomePageImage> HomePageImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<RequestPay> RequestPays { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.RequestPay)
                .WithMany(u => u.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            SeedData(modelBuilder);

            //اعمال ایندکس برروی فیلد ایمیل
            //اعمال عدم تکراری بودن ایمیل
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            ApplyQueryFilter(modelBuilder);
        }

        protected void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            //مشخص میشود که کاربرانی ستون حذف شدگی ان فعال بود را برای ما لود نکند
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductFeatures>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HomePageImage>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CartItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<RequestPay>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(p => !p.IsRemoved);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // افزودن مقادیر پیش فرض به جدول
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRoles.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRoles.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRoles.Customer) });
        }
    }
}