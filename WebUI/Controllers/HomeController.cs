using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Model;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repo)
        {
            repository = repo;
        }

        //Home
        public ViewResult Index()
        {
            return View();
        }

        //Home/AboutCompany
        public ViewResult AboutCompany()
        {
            ViewBag.NamePage = "О Компании"; // необходимо для идентификации текущей страницы сайта, в представлении ("_Menu")
            return View("AboutCompany");
        }

        //Home/ShippinAndPayment
        public ViewResult ShippinAndPayment()
        {
            ViewBag.NamePage = "Доставка и оплата"; // необходимо для идентификации текущей страницы сайта, в представлении ("_Menu")
            return View("ShippinAndPayment");
        }

        //Home/AboutTehnology
        public ViewResult AboutTehnology()
        {
            return View("AboutTehnology");
        }

        //Home/ForSuppliers
        public ViewResult ForSuppliers()
        {
            return View("ForSuppliers");
        }

        //Home/OurDocuments
        public ViewResult OurDocuments()
        {
            return View("OurDocuments");
        }

        //Home/EcologicalyStandarts
        public ViewResult EcologicalyStandarts()
        {
            return View("EcologicalyStandarts");
        }

        #region Частичные представления на страницы Home

        [ChildActionOnly]
        public PartialViewResult Hits()
        {
            List<IceCream> HitsIceCream = repository.GetAllHitsIceCreams().ToList();
            return PartialView("_HitsProducts", HitsIceCream);
        }

        #endregion

        // TODO: можно попробовать вынести методы из HomeController
        #region Методы используемые в различных частях проекте

        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            return PartialView("_Menu");
        }

        [ChildActionOnly]
        public PartialViewResult UserCart()
        {
            ViewBag.ItemInCart = 0;
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                ViewBag.ItemInCart = cart.Items.Count;
            }

            return PartialView("_UserCart");
        }

        public FileContentResult GetImage(int id)
        {
            IceCream iceCream = repository.GetIceCream(id);

            if (iceCream.Image != null)
            {
                return File(iceCream.Image.ImageData, iceCream.Image.ImageMimeType);
            }
            else
            {
                return null;
            }
        }       
        
        #endregion

    }
}