using Microsoft.EntityFrameworkCore;
using ServiceShop.Domain.Entities;
using ServiceShop.Domain.Repositories.Abstract;

namespace ServiceShop.Domain.Repositories.EntityFramework
{
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        public readonly AppDbContext context;
        public EFServiceItemsRepository(AppDbContext context) { this.context = context; }
        public void DeleteServiceItem(Guid id)
        {
            context.ServiceItems.Remove(new ServiceItem { Id = id });
            context.SaveChanges();
        }

        public ServiceItem GetServiceItem(Guid id)
        {
            return context.ServiceItems.FirstOrDefault(s=>s.Id==id);
        }

        public IQueryable<ServiceItem> GetServiceItems()
        {
            return context.ServiceItems;
        }

        public void SaveServiceItem(ServiceItem entity)
        {
            if (entity.Id==default)
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
                return;
            }
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
