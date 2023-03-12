using Microsoft.AspNetCore.Mvc;
using ServiceShop.Domain;
using System.Runtime.CompilerServices;

namespace ServiceShop.Models.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly DataManager dataManager;
        public SideBarViewComponent(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult((IViewComponentResult) View("Default", dataManager.ServiceItems!.GetServiceItems()));
        }
    }
}
