using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Online_Food_Corner.Models;

namespace Online_Food_Corner.Controllers
{
    public class AdminController : Controller
    {
        private OnlineFoodCornerModelEntities db;
        //Constructor
        public AdminController()
        {
            db = new OnlineFoodCornerModelEntities();
        }

        //Destructor
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }


        // Admin Dashboard
        public ActionResult AdminDashboard()
        {
            return View();
        }

        //User Dashboard
        public ActionResult UserDashboard()
        {
            var products = db.Products.ToList();
            return View(products);
        }
        //Menu
        public ActionResult Menu()
        {
            return View();
        }

        //Product List
        public ActionResult ProductList()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        //Add Product
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product model, HttpPostedFileBase pImage)
        {
            if (db.Products.Any(c => c.product_name == model.product_name))
            {
                ViewBag.Error = "Name already present!";
                return View(model);
            }
            if (pImage != null && pImage.ContentLength > 0)
            {
                model.product_picture = new byte[pImage.ContentLength];
                pImage.InputStream.Read(model.product_picture, 0, pImage.ContentLength);
            }
            db.Products.Add(model);
            db.SaveChanges();
            return RedirectToAction("ProductList");

        }

        //Edit Product
        public ActionResult EditProduct(int id)
        {
            var model = db.Products.SingleOrDefault(x => x.product_id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(int id, Product model, HttpPostedFileBase pImage)
        {
            var product = db.Products.Single(x => x.product_id == id);
            if (pImage != null && pImage.ContentLength > 0)
            {
                model.product_picture = new byte[pImage.ContentLength];
                pImage.InputStream.Read(model.product_picture, 0, pImage.ContentLength);
            }
            product.product_name = model.product_name;
            product.product_type = model.product_type;
            product.product_Price = model.product_Price;
            product.product_picture = model.product_picture;
            product.product_quantity = model.product_quantity;
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }

        // Product Details
        public ActionResult ProductDetails(int id)
        {
            var product = db.Products.SingleOrDefault(x => x.product_id == id);
            return View(product);
        }

        //Delete Product
        public ActionResult ProductDelete(int? id)
        {
            var product = db.Products.Single(x => x.product_id == id);
            return View(product);
        }
        [HttpPost, ActionName("ProductDelete")]
        public ActionResult ProductDelete(int id)
        {
            var product = db.Products.SingleOrDefault(x => x.product_id == id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }


        //Product Work For User
        public ActionResult ProductListReadOnly()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        //Order List
        public ActionResult OrderList()
        {
            var order = db.Orders.ToList();
            return View(order);
        }
        //Add Order
        public ActionResult AddOrder(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder(int id, Order model)
        {
            var product = db.Products.SingleOrDefault(x => x.product_id == id);
            model.product_id = id;
            //model.customer_id = User.Identity.GetUserId();
            model.timing = DateTime.Now;
            model.order_date = DateTime.Now;
            model.total_price = 100;
            db.Orders.Add(model);
            db.SaveChanges();
            return View(model);
        }
    }
}
