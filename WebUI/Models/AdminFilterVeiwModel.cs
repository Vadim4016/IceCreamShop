using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class AdminFilterVeiwModel
    {
        // поиск по слову
        [Display(Name = "Поиск по слову")]
        public string Word { get; set; }

        //поиск по цене
        [Display(Name = "Цена")]
        public int? Price { get; set; }

        //поиск по жирности продукта
        [Display(Name = "Жирность")]
        public int? Fat { get; set; }

        //показать список лучших товаров
        [Display(Name = "Лучшие товары")]
        public bool Hits { get; set; }
    }
}