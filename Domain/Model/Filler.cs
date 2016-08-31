using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    // наполнители
    public class Filler
    {
        [Display(Name = "Шоколадные наполнители")]
        public bool Сhocolate { get; set; }
        [Display(Name = "Сахарные присыпки")]
        public bool SugarPowder { get; set; }
        [Display(Name = "Фрукты")]
        public bool Fruit { get; set; }
        [Display(Name = "Сиропы")]
        public bool Syrups { get; set; }
        [Display(Name = "Джемы")]
        public bool Jams { get; set; }
    }
}
