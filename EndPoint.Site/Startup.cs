using System;
using System.IO;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPatterns.Product;
using Store.Application.Services.Carts;
using Store.Application.Services.Common.Queries.GetCategory;
using Store.Application.Services.Common.Queries.GetMenuItem;
using Store.Application.Services.Common.Queries.GetSlider;
using Store.Application.Services.Common.Queries.GetSliderMenu;
using Store.Application.Services.Finances.CreateRequestPay;
using Store.Application.Services.Finances.GetRequestPay;
using Store.Application.Services.Finances.GetRequestPayForAdmin;
using Store.Application.Services.HomePages.AddHomePageImages;
using Store.Application.Services.HomePages.AddNewSlider;
using Store.Application.Services.HomePages.DeleteSlider;
using Store.Application.Services.HomePages.EditSlider;
using Store.Application.Services.HomePages.GetHomePageImages;
using Store.Application.Services.Orders.AddNewOrder;
using Store.Application.Services.Orders.CancelOrder;
using Store.Application.Services.Orders.GetOrdersForAdmin;
using Store.Application.Services.Orders.GetUserOrder;
using Store.Application.Services.Products.FacadPattern;
using Store.Application.Services.Users.Commands.EditUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Commands.RemoveUser;
using Store.Application.Services.Users.Commands.UserLogin;
using Store.Application.Services.Users.Commands.UserSatusChange;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Application.Services.Users.Queries.GetUserById;
using Store.Application.Services.Users.Queries.GetUsers;
using Store.Common.Roles;
using Store.Infrastructure.Zarinpal;
using Store.Persistence.Contexts;
using ZarinPal.Class;

namespace EndPoint.Site;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(
                "D:\\Amir\\Projects\\CouresesCodes\\C# Codes\\Bugto\\ASP.NET Core Bugto elemntry\\season6\\Store\\keys"))
            .SetApplicationName("Store");

        services.AddAuthorization(options =>
        {
            options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
            options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
            options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
        });

        services.AddAuthentication(options =>
        {
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }).AddCookie(options =>
        {
            options.LoginPath = new PathString("/Authentication/Signin");
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            options.AccessDeniedPath = new PathString("/Authentication/Signin");
        });


        services.AddScoped<IDataBaseContext, DataBaseContext>();
        services.AddScoped<IGetUsersService, GetUsersService>();
        services.AddScoped<IGetRolesService, GetRolesService>();
        services.AddScoped<IRegisterUserService, RegisterUserService>();
        services.AddScoped<IRemoveUserService, RemoveUserService>();
        services.AddScoped<IUserSatusChangeService, UserSatusChangeService>();
        services.AddScoped<IEditUserService, EditUserService>();
        services.AddScoped<IUserLoginService, UserLoginService>();

        //FacadeInject
        services.AddScoped<IProductFacad, ProductFacad>();

        //------------------------
        services.AddScoped<IGetMenuItemService, GetMenuItemService>();
        services.AddScoped<IGetCategoryService, GetCategoryService>();
        services.AddScoped<IGetSliderMenu, GetSliderMenu>();

        services.AddScoped<IAddNewSliderService, AddNewSliderService>();
        services.AddScoped<IGetSliderService, GetSliderService>();
        services.AddScoped<IDeleteSliderService, DeleteSliderService>();
        services.AddScoped<IEditSliderService, EditSliderService>();
        services.AddScoped<IAddHomePageImagesService, AddHomePageImagesService>();
        services.AddScoped<IGetHomePageImagesService, GetHomePageImagesService>();

        services.AddScoped<ICartService, CartService>();
        services.AddScoped<CookieManager>();

        services.AddHttpClient();

        services.AddScoped<ICreateRequestPayService, CreateRequestPayService>();
        services.AddScoped<IGetRequestPayService, GetRequestPayService>();
        services.AddSingleton<Expose>();
        services.AddScoped<IZarinpalPaymentService, ZarinpalPaymentService>();

        services.AddScoped<IAddNewOrderService, AddNewOrderService>();
        services.AddScoped<IGetUserOrderService, GetUserOrderService>();
        services.AddScoped<ICancelOrderService, CancelOrderService>();
        services.AddScoped<IGetRequestPayForAdminService, GetRequestPayForAdminService>();

        services.AddScoped<IGetOrdersForAdminService, GetOrdersForAdminService>();
        services.AddScoped<IGetUserByIdService, GetUserByIdService>();

        var connectionString = @"Data Source=.;Initial Catalog=Store_DB;Integrated Security=True;";

        //services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("EndPoint.Site")));
        services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionString));
        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}"
            );
            endpoints.MapControllerRoute(
                "areas",
                "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
        });
    }
}