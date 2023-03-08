using Microsoft.AspNetCore.Mvc;

namespace ServiceShop.Controllers
{
	public class HomeController:Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
