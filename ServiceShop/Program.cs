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
                opts.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); }); //add policy
            });
            builder.Services.AddControllersWithViews(opts =>
            {
                opts.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea")); //use policy
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddResponseCompression( //defualt response compression alghoritm
                options =>
                {
                    options.EnableForHttps = true;
                });

            builder.Services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            builder.Services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            builder.Services.AddTransient<DataManager>(); //shared repository for above services 

            builder.Services.AddDbContext<AppDbContext>(); //config inside AppDbContext

            //userIdentity config
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

            app.UseResponseCompression();
            //css,js, etc
            app.UseStaticFiles(new StaticFileOptions()
            {
                //caches static files (asp-append-version used in css/js views)
                OnPrepareResponse = ctx => ctx.Context.Response.Headers.Add("Cache-control", "public,max-age=600") 
            }) ; 
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}", defaults: new { controller = "Home", action = "Index" });
            app.Run();
        }
    }
}