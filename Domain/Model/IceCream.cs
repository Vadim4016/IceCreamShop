using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class IceCream
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public int Price { get; set; }
        
        [Display(Name = "Изображение")]
        public Image Image { get; set; }

        [Required]
        [Display(Name = "Жирность")]
        public int Fat { get; set; }

        [Display(Name = "Хит продаж")]
        public bool Hit { get; set; }

        [Display(Name = "Наполнители")]
        public Filler Filler { get; set; }
    }
}
