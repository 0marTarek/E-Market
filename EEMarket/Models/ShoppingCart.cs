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

namespace EEMarket.Models
{
    public partial class ShoppingCart
    {
        DB_CONTEXT db = new DB_CONTEXT();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "card_id";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(product product)
        {
            var cartItem = db.cart.SingleOrDefault(c => c.card_id == ShoppingCartId && c.ProductId == product.ID);
            cartItem = new Cart
            {
                    ProductId = product.ID,
                    card_id = ShoppingCartId,
                    add_at = DateTime.Now
            };
            db.cart.Add(cartItem);
            db.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = db.cart.Single(cart => cart.card_id == ShoppingCartId && cart.item_id == id);

            int itemCount = 0;

            db.cart.Remove(cartItem);
                
         
             db.SaveChanges();
            
            return itemCount;
        }
        //retrive current session card
        //http get
        public List<Cart> GetCartItems()
        {
            return db.cart.Where(cart => cart.card_id == ShoppingCartId).ToList();
        }
        //retrive current session card
        //http post
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }

            }

            return context.Session[CartSessionKey].ToString();
        }
    }
}