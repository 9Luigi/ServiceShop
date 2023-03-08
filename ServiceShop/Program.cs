namespace ServiceShop
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			#region Services
			builder.Services.AddControllersWithViews();
			#endregion
			var app = builder.Build();

			app.UseStaticFiles();

			app.MapControllerRoute
				(
				name: "default",
				pattern: "{controller}/{action}/{id?}",
				defaults: new { controller = "Home", action = "Index" }
				);

			app.Run();
		}
	}
}