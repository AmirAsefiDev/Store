
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Roles;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Contexts
{
    public class DataBaseContext:DbContext,IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
