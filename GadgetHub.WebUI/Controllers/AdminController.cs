using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IGadgetRepository repository;

        public AdminController(IGadgetRepository repository)
        {
            this.repository = repository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.Gadgets);
        }

        public ViewResult Edit(int productId)
        {
            Gadget product = repository.Gadgets.FirstOrDefault(p => p.ProductId == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Gadget product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                repository.SaveGadget(product);
                TempData["message"] = string.Format ("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Gadget());
        }

        [HttpPost]
        public ActionResult Delete(int productId) { 
            Gadget deletedProduct = repository.DeleteGadget(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}