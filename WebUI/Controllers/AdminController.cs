using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Model;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IRepository repository;

        public AdminController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: Admin
        public ActionResult Index(AdminFilterVeiwModel filter)
        {
            var iceCreams = repository.GetAllIceCreams();
            if (!string.IsNullOrEmpty(filter.Word))
            {
                iceCreams = iceCreams.Where(i => i.Name.Contains(filter.Word));
            }
            if (filter.Price != null)
            {
                iceCreams = iceCreams.Where(i => i.Price == filter.Price);
            }
            if (filter.Fat != null)
            {
                iceCreams = iceCreams.Where(i => i.Fat == filter.Fat);
            }
            if (filter.Hits)
            {
                iceCreams = iceCreams.Where(i => i.Hit);
            }

            return View(iceCreams.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var iceCream = repository.GetIceCream((int)id);
            if (iceCream == null)
            {
                return HttpNotFound();
            }
            return View(iceCream);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Id")]IceCream iceCream, HttpPostedFileBase upFile = null)
        {
            if (ModelState.IsValid)
            {
                if (upFile != null)
                {
                    Image image = new Image
                    {
                        ImageMimeType = upFile.ContentType,
                        ImageData = new byte[upFile.ContentLength]
                    };
                    upFile.InputStream.Read(image.ImageData, 0, upFile.ContentLength);
                    iceCream.Image = image;
                }

                repository.AddIceCream(iceCream);
                return RedirectToAction("Index");
            }

            return View(iceCream);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IceCream iceCream = repository.GetIceCream((int) id);
            if (iceCream == null)
            {
                return HttpNotFound();
            }
            return View(iceCream);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IceCream iceCream, HttpPostedFileBase upFile = null)
        {
            if (ModelState.IsValid)
            {
                if (upFile != null)
                {
                    Image image = new Image
                    {
                        ImageMimeType = upFile.ContentType,
                        ImageData = new byte[upFile.ContentLength]
                    };
                    upFile.InputStream.Read(image.ImageData, 0, upFile.ContentLength);
                    iceCream.Image = image;
                }
                repository.EditIceCream(iceCream);
                return RedirectToAction("Index");
            }
            return View(iceCream);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IceCream iceCream = repository.GetIceCream((int) id);
            if (iceCream == null)
            {
                return HttpNotFound();
            }
            return View(iceCream);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteIceCream(id);
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            return PartialView("_Menu");
        }

        [ChildActionOnly]
        public PartialViewResult Filter(AdminFilterVeiwModel filter)
        {
            return PartialView("_FilterCatalog");
        }
    }
}
