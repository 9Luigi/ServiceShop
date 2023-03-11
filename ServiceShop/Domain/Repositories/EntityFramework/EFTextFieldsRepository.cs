using Microsoft.EntityFrameworkCore;
using ServiceShop.Domain.Entities;
using ServiceShop.Domain.Repositories.Abstract;

namespace ServiceShop.Domain.Repositories.EntityFramework
{
    public class EFTextFieldsRepository : ITextFieldsRepository
    {
        public readonly AppDbContext context;
        public EFTextFieldsRepository(AppDbContext context) { this.context = context; }
        public void DeleteTextField(Guid id)
        {
            context.TextFields.Remove(new TextField() { Id = id });
            context.SaveChanges();
        }

        public TextField GetTextFieldById(Guid id) => context.TextFields.FirstOrDefault(tf => tf.Id == id)!;

        public IQueryable<TextField> GetTextFields()
        {
            return context.TextFields;
        }

        public TextField GetTextFieldByCodeWord(string codeWord)
        {
            return context.TextFields.FirstOrDefault(tf => tf.CodeWord == codeWord)!;
        }

        public void SaveTextField(TextField entity)
        {
            if (entity.Id == default)
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
