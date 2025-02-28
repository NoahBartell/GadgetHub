using System;
using System.Collections.Generic;
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
        public ViewResult List(int page = 1)
        {
            GadgetListViewModel model = new GadgetListViewModel {
                Gadgets = myGadgetRepository.Gadgets.OrderBy(p => p.ProductId).Skip((page - 1) * pageSize).Take(pageSize),

               PagingInfo = new PagingInfo
               {
                   CurrentPage = page,
                   ItemsPerPage = pageSize,
                   TotalItems = myGadgetRepository.Gadgets.Count()
               }
            };
            return View(model);
        }
    }
}