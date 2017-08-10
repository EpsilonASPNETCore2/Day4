using EComm.MVC.Models;
using EComm.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.MVC.Controllers
{
    public class ProductController : Controller
    {
        private ECommData ECommData { get; }
        public ProductController(ECommData ecommData)
        {
            ECommData = ecommData;
        }

        public IActionResult Index()
        {
            return View(ECommData.GetProducts());
        }

        public async Task<IActionResult> Async()
        {
            return View("Index", await ECommData.GetProductsAsync());
        }

        public IActionResult Detail(int id)
        {
            //ViewBag.guid1 = ECommData.ToString();
            //ViewBag.guid2 = ECommData.ToString();

            var product = ECommData.GetProduct(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var product = ECommData.GetProduct(id);
            var totalCost = quantity * product.UnitPrice;
            string message = $"You added {product.ProductName} " +
                $"(x{quantity}) to your cart at a total cost of {totalCost:C}.";

            var cart = ShoppingCart.GetFromSession(HttpContext.Session);
            var lineItem = cart.LineItems.SingleOrDefault(item =>
            item.Product.Id == id);
            if (lineItem != null) lineItem.Quantity += quantity;
            else
                cart.LineItems.Add(new ShoppingCart.LineItem { Product = product, Quantity = quantity });
            
            ShoppingCart.StoreInSession(cart, HttpContext.Session);

            return PartialView("_AddedToCart", message);
        }

        public IActionResult Cart()
        {
            var cart = ShoppingCart.GetFromSession(HttpContext.Session);
            var cvm = new CartViewModel() { Cart = cart };
            return View(cvm);
        }

        [HttpPost]
        public IActionResult Checkout(CartViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                cvm.Cart = ShoppingCart.GetFromSession(HttpContext.Session);
                return View("Cart", cvm);
            }
            // TODO: Charge the customer's card and record the order
            HttpContext.Session.Clear();
            return View("ThankYou");
        }
    }
}
