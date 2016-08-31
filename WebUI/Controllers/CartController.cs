using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Model;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IRepository repository;

        public CartController(IRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            CartViewModel viewModel = new CartViewModel {Cart = GetCart()};
            return View(viewModel);
        }

        public RedirectToRouteResult AddToCart(int iceCreamId)
        {
            IceCream iceCream = repository.GetIceCream(iceCreamId);

            if (iceCream != null)
            {
                GetCart().AddItem(iceCream, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToRouteResult RemoveFromCart(int iceCreamId)
        {
            IceCream iceCream = repository.GetIceCream(iceCreamId);

            if (iceCream != null)
            {
                GetCart().RemoveLine(iceCream);
            }

            return RedirectToAction("Index");
        }

        public RedirectToRouteResult ClearCart()
        {
            GetCart().Clear();
            return RedirectToAction("Index");
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
    }
}