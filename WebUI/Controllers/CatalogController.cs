using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Model;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CatalogController : Controller
    {
        public int ItemsCount = 12; // Параметр который устанавливает кол-во товара на странице
        private IRepository repository;

        public CatalogController(IRepository repo)
        {
            repository = repo;
        }

        // GET: Catalog
        public ViewResult Index()
        {
            // необходимо для идентификации текущей страницы сайта, в представлении ("_Menu")
            ViewBag.NamePage = "Каталог";
            return View();
        }

        #region Частичные представления для страницы Catalog

        public PartialViewResult CatalogProducts(int page = 1, FilterViewModel filter = null)
        {
            var iceCreams = repository.GetAllIceCreams().ToList();
            IFilter filterCatallog = new FilterIceCream();
            if (filter != null)
            {
                iceCreams = filterCatallog.FilterByFat(iceCreams, 0, filter.Fat);
                if (filter.Filler != null) iceCreams = filterCatallog.FilterByFillers(iceCreams, filter.Filler);
                iceCreams = filterCatallog.FilterByOderCost(iceCreams, filter.OrderSortCost);
            }

            PageInfo pageInfo = GetPageInfo(page, iceCreams);
            iceCreams = GetResultIceCream(page, pageInfo, iceCreams);

            CatalogViewModel model = new CatalogViewModel
            {
                IceCreams = iceCreams,
                filterModel = filter,
                pageInfoModel = pageInfo
            };

            return PartialView("_CatalogProducts", model);
        }

        #endregion

        #region Вспомогательные методы

        private PageInfo GetPageInfo(int page, List<IceCream> iceCreams)
        {
            PageInfo pageInfo = new PageInfo
            {
                ItemOnPage = this.ItemsCount,
                TotalPage = (int)Math.Ceiling((decimal)iceCreams.Count / this.ItemsCount),
                CarrentPage = page,
                TotalItem = iceCreams.Count
            };

            return pageInfo;
        }

        private List<IceCream> GetResultIceCream(int page, PageInfo pageInfo, List<IceCream> iceCreams)
        {
            return iceCreams.Skip((page - 1) * pageInfo.ItemOnPage).Take(pageInfo.ItemOnPage).ToList();
        }
        
        #endregion
    }
}