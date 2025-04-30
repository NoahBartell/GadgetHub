using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class GadgetController : Controller
    {

        private IGadgetRepository myGadgetRepository;
        // GET: Gadget
        public GadgetController(IGadgetRepository gadgetRepository) {
            this.myGadgetRepository = gadgetRepository;
        }

        int pageSize = 4;
        public ViewResult List(string category, int page = 1)
        {
            GadgetListViewModel model = new GadgetListViewModel
            {
                Gadgets = myGadgetRepository.Gadgets
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? myGadgetRepository.Gadgets.Count() : myGadgetRepository.Gadgets.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);

            //return View(myProductRepository.Products.OrderBy(p => p.ProductId).Take(PageSize)
            //    .Skip((page-1) * PageSize));
        }

        public FileContentResult GetImage(int productId)
        {
            var product = myGadgetRepository.Gadgets.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}