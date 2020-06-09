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
    public class productController : Controller
    {
        DB_CONTEXT db = new DB_CONTEXT();
        public ActionResult CartIndex()
        {
            List<Cart> cartlist = db.cart.ToList();
            return View(cartlist);
        }
        public ActionResult Popup() 
        {
            return PartialView();
        }

        public ActionResult AddToCart(int id)
        {
            // ProductModel productModel = new ProductModel();
            //check if session with cart is empty
            var cart1 = new Cart();
            cart1.ProductId = id;
            cart1.add_at = DateTime.Now;
            var cart2 = db.cart.Find(id);
            if (cart2 != null)
            {
                return RedirectToAction("ListProducts");
            }
            else
            {
                db.cart.Add(cart1);
                db.SaveChanges();
                return RedirectToAction("ListProducts");
            }
        }
       

        [HttpPost]
        public ActionResult Remove(int id)
        {

            var cart = db.cart.Where(x => x.ProductId == id).FirstOrDefault();
            string productName = db.product.Single(item => item.ID == id).Name;
            var results = new RemovedCard
            {
                Message = Server.HtmlEncode(productName) + "has been removed from your shopping cart.",
                // CartTotal = cart.GetTotal(),
                // CartCount = cart.GetCount(),
               // ItemCount = itemCount,
                DeleteId = id
            };
            db.cart.Remove(cart);
            db.SaveChanges();
            return Json(results);
        }


        [HttpGet]
        public ActionResult AddProduct()
        {

            var Category = db.category.ToList();

            ProductCategoryView pcv = new ProductCategoryView
            {
                category = Category
            };

            return View(pcv);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductCategoryView pcv, HttpPostedFileBase upload)
        {

            string path = Path.Combine(Server.MapPath("~/UPloads"), upload.FileName);
            upload.SaveAs(path); //file has been saved in server but not in db
            pcv.Product.Image = upload.FileName;
            db.product.Add(pcv.Product);
            db.SaveChanges();
            return RedirectToAction("ListProducts");
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var product = db.product.Single(c => c.ID == id);
            var Category = db.category.ToList();
            ProductCategoryView pcv = new ProductCategoryView
            {
                category = Category,
                Product = product
            };
            return View(pcv);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductCategoryView pcv, HttpPostedFileBase upload)
        {
            string path = Path.Combine(Server.MapPath("~/UPloads"), upload.FileName);
            upload.SaveAs(path); //file has been saved in server but not in db
            pcv.Product.Image = upload.FileName;

            var product = db.product.Single(c => c.ID == pcv.Product.ID);
            product.Name = pcv.Product.Name;
            product.Price = pcv.Product.Price;
            product.Image = pcv.Product.Image;
            product.Description = pcv.Product.Description;
            product.CategoryId = pcv.Product.CategoryId;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = pcv.Product.ID });

        }

        [HttpGet]
        public ActionResult ListProducts()
        {
            List<product> Allproducts = new List<product>();

            Allproducts = db.product.ToList();
            ViewBag.Porduct = Allproducts;
            return View();
        }
       
        public ActionResult Cart()
        {
            List <Cart> model = db.cart.ToList();
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult ListProducts(String FilterText)
        {
            List<product> Allproducts = new List<product>();

            //if the search field is empty.
            if (FilterText == "")
            {
                Allproducts = db.product.ToList();
                ViewBag.Porduct = Allproducts;
                return View();
            }
            Category FilterId = null;
            try
            {
                FilterId = db.category.Single(Category => Category.Name == FilterText);
            }
            catch (Exception e)
            {
                Allproducts = db.product.ToList();
                ViewBag.Porduct = Allproducts;
                return View();
            }
            int TheID;
            if (FilterId == null)
            {
                Allproducts = db.product.ToList();
                ViewBag.Porduct = Allproducts;
                return View();
            }
            TheID = FilterId.Id;
            Allproducts = db.product.Where(product => product.CategoryId == TheID).ToList();

            //Allproducts = db.product.ToList();
            ViewBag.Porduct = Allproducts;
            return View();
        }
        [HttpGet]

        public ActionResult Details(int id)
        {
            product product = db.product.Find(id);
            if (product == null)
            {
                return View(product);
            }

            return View(product);
        }
    }
}
