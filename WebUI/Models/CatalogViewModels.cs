using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace WebUI.Models
{
    public class FilterViewModel
    {
        [Required]
        public string OrderSortCost { get; set; }
        public int Fat { get; set; }
        public Filler Filler { get; set; }
    }

    public class PageInfo
    {
        public int TotalPage { get; set; }
        public int CarrentPage { get; set; }
        public int TotalItem { get; set; }
        public int ItemOnPage { get; set; }
    }

    public class CatalogViewModel
    {
        public List<IceCream> IceCreams { get; set; }
        public FilterViewModel filterModel { get; set; }
        public PageInfo pageInfoModel { get; set; }
    }
}