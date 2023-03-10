using System.ComponentModel.DataAnnotations;

namespace ServiceShop.Domain.Entities
{
    public class ServiceItem : EntityBase
    {
        [Required(ErrorMessage ="Fill service title")]
        [Display(Name ="Title")]
        public override string? Title { get; set; }

        [Display(Name = "Short service description")]
        public override string? Subtitle { get; set; }

        [Display(Name = "Full service description")]
        public override string? Text { get; set; }

    }
}
