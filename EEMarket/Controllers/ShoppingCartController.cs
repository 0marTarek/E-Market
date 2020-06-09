/*

using EEMarket.context;
using EEMarket.ViewModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using EEMarket.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace EEMarket.Controllers
{
    public class ShoppingCartController : Controller
    {
        DB_CONTEXT db = new DB_CONTEXT();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new CardViewModel
            {
             //   CartItems = cart.GetCartItems(),
                //CartTotal = cart.GetTotal()
            };
            return View(viewModel);
        }
        public ActionResult AddToCart(int id)
        {
            var addedProduct = db.product.Single(product => product.ID == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedProduct);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
         //   string productName = db.cart.Single(item => item.item_id == id).product.Name;
            int itemCount = cart.RemoveFromCart(id);
            var results = new RemovedCard
            {
              //  Message = Server.HtmlEncode(productName) + "has been removed from your shopping cart.",
               // CartTotal = cart.GetTotal(),
               // CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        
    }
} 
*/