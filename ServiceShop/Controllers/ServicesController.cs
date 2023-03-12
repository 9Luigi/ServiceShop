using Microsoft.AspNetCore.Mvc;
using ServiceShop.Domain;

namespace ServiceShop.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DataManager dataManager;
        public ServicesController(DataManager dataManager) { this.dataManager = dataManager; }
        public ActionResult Index(Guid id)
        {
            if (id != default)
            {
                return View("Show", dataManager.ServiceItems!.GetServiceItemByID(id));
            }
            ViewBag.TextField = dataManager.TextFields!.GetTextFieldByCodeWord("PageServices");
            return View(dataManager.ServiceItems!.GetServiceItems());
        }
    }
}
