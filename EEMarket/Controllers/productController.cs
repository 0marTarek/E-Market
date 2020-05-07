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
            if ( FilterId == null)
            {
                Allproducts = db.product.ToList();
                ViewBag.Porduct = Allproducts;
                return View();
            }
            TheID= FilterId.Id;
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
