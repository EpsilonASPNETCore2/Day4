using EComm.MVC.Models;
using EComm.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.MVC.Controllers
{ 
    public class ProductController : Controller
    {
        private ECommData ECommData { get; }
        private ILogger<ProductController> Logger { get; }
        public ProductController(ECommData ecommData, ILogger<ProductController> logger)
        {
            ECommData = ecommData;
            Logger = logger;
        }

        public IActionResult Index()
        {
            Logger.LogInformation("*** Index(): Called");
            return View(ECommData.GetProducts());
        }

        public async Task<IActionResult> Async()
        {
            Logger.LogInformation("*** Async(): Called");
            return View("Index", await ECommData.GetProductsAsync());
        }

        public IActionResult Detail(int id)
        {
            //ViewBag.guid1 = ECommData.ToString();
            //ViewBag.guid2 = ECommData.ToString();

            var product = ECommData.GetProduct(id);
            if (product == null)
            {
                Logger.LogError("!!! Detail({id}): Not Found", id);
                return NotFound();
            }
            Logger.LogInformation("*** Detail({id}): Found", id);
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
            var lineItem = cart.LineItems.SingleOrDefault(item => item.Product.Id == id);
            if (lineItem != null) lineItem.Quantity += quantity;
            else cart.LineItems.Add(new ShoppingCart.LineItem
                        { Product = product, Quantity = quantity });
            
            ShoppingCart.StoreInSession(cart, HttpContext.Session);
            Logger.LogInformation("*** AddToCart({id}, {quantity}): Cart Updated", id, quantity);

            return PartialView("_AddedToCart", message);
        }

        public IActionResult Cart()
        {
            var cart = ShoppingCart.GetFromSession(HttpContext.Session);
            var cvm = new CartViewModel() { Cart = cart };
            Logger.LogInformation("*** Cart(): Called");
            return View(cvm);
        }

        [HttpPost]
        public IActionResult Checkout(CartViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                cvm.Cart = ShoppingCart.GetFromSession(HttpContext.Session);

                Logger.LogWarning("!!! Checkout(CartViewModel): !ModelState.IsValid");
                return View("Cart", cvm);
            }
            // TODO: Charge the customer's card and record the order
            HttpContext.Session.Clear();
            Logger.LogInformation("*** Checkout(CartViewModel): Checkout Complete");
            return View("ThankYou");
        }
         
        public void LoggingScopesExample()
        {
            // Must enable scopes:  IncludeScopes: true in appsettings or Startup.cs
            // Not working in Preview 2
            using (Logger.BeginScope("### IncludeScopes Example"))
            {
                Logger.LogInformation("LoggingScopesExample(): Logger.LogInformation");
                Logger.LogWarning("LoggingScopesExample(): Logger.LogWarning");
                Logger.LogError("LoggingScopesExample(): Logger.LogError");
                Logger.LogCritical("LoggingScopesExample(): Logger.LogCritical");
            }
        }
    }
}
