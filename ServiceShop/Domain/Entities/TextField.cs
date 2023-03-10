using System.ComponentModel.DataAnnotations;

namespace ServiceShop.Domain.Entities
{
    public class TextField : EntityBase
    {
        [Required]
        public string? CodeWord { get; set; }

        [Display(Name = "Title")]
        public override string? Title { get; set; } = "Info page";

        [Display(Name = "Content")]
        public override string? Text { get; set; } = "Content by admin";
    }
}
