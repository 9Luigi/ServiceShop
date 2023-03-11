using Microsoft.AspNetCore.Identity;
using ServiceShop.Domain;
using ServiceShop.Domain.Repositories.Abstract;
using ServiceShop.Domain.Repositories.EntityFramework;
using ServiceShop.Service;

namespace ServiceShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Services
            builder.Services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });
            builder.Services.AddControllersWithViews(opts =>
            {
                opts.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
            });

            builder.Services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            builder.Services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            builder.Services.AddTransient<DataManager>();

            builder.Services.AddDbContext<AppDbContext>(); //config inside AppDbContext

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "CompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });
            #endregion
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}", defaults: new { controller = "Home", action = "Index" });
            app.Run();
        }
    }
}