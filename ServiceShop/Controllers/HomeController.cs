using Microsoft.AspNetCore.Mvc;
using ServiceShop.Domain;

namespace ServiceShop.Controllers
{
	public class HomeController:Controller
	{
		private readonly DataManager dataManager;
		public HomeController(DataManager dataManager)
		{
			this.dataManager = dataManager;
		}
		public IActionResult Index()
		{
			return View(dataManager.TextFields!.GetTextFieldByCodeWord("PageIndex")); //return model with data from db
		}
		public IActionResult Contacts()
		{
			return View(dataManager.TextFields!.GetTextFieldByCodeWord("PageContacts"));
		}
	}
}
