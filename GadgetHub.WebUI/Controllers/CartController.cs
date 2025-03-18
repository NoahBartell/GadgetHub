using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IGadgetRepository repository;
        private IOrderProcessor orderProcessor;

        // GET: Cart
        public CartController(IGadgetRepository repository, IOrderProcessor orderProcessor)
        {
            this.repository = repository;
            this.orderProcessor = orderProcessor;
        }

        private Cart GetCart(){
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Gadget gadget = repository.Gadgets.FirstOrDefault(p => p.ProductId == productId);
            if (gadget != null)
            {
                cart.AddItem(gadget, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Gadget gadget = repository.Gadgets.FirstOrDefault(p => p.ProductId == productId);
            if (gadget != null)
            {
                cart.RemoveLine(gadget);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed", shippingDetails);
            }
            else
            {
                return View(shippingDetails);
            }
        }



    }
}