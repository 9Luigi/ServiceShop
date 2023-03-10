using System.ComponentModel.DataAnnotations;

namespace ServiceShop.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase() => DateAdded = DateTime.UtcNow;

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public virtual string? Title { get; set; }

        [Display(Name = "Short description")]
        public virtual string? Subtitle { get; set; }

        [Display(Name = "Full description")]
        public virtual string? Text { get; set; }

        [Display(Name = "Title image")]
        public virtual string? TitleImagePath { get; set; }

        [Display(Name = "Seo metatag Title")]
        public virtual string? MetaTitle { get; set; }

        [Display(Name = "Seo metatag Description")]
        public virtual string? MetaDescription { get; set; }

        [Display(Name = "Seo metatag Keywords")]
        public virtual string? MetaKeywords { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }

    }
}
