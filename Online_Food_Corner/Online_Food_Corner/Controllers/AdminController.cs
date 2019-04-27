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
        private ApplicationDbContext db;
        //Constructor
        public AdminController()
        {
            db = new ApplicationDbContext();
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
            var model = db.Products.SingleOrDefault(x => x.id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(int id, Product model, HttpPostedFileBase pImage)
        {
            var product = db.Products.Single(x => x.id == id);
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
            var product = db.Products.SingleOrDefault(x => x.id == id);
            return View(product);
        }

        //Delete Product
        public ActionResult ProductDelete(int? id)
        {
            var product = db.Products.Single(x => x.id == id);
            return View(product);
        }
        [HttpPost, ActionName("ProductDelete")]
        public ActionResult ProductDelete(int id)
        {
            var product = db.Products.SingleOrDefault(x => x.id == id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }

        //Order List
        public ActionResult OrderList()
        {
            var order = db.Orders.ToList();
            return View(order);
        }

        //Create Chef
        public ActionResult CreateEmployee()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Worker model)
        {
            db.Workers.Add(model);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }
        //Employee List
        public ActionResult EmployeeList()
        {
            var employee = db.Workers.ToList();
            return View(employee);
        }
        //Edit Employee
        public ActionResult EditEmployee(int id)
        {
            var model = db.Workers.SingleOrDefault(x => x.id == id);
            return View(model);
        } 
        [HttpPost]
        public ActionResult EditEmployee(int id, Worker model)
        {
            var worker = db.Workers.Single(x => x.id == id);
            
            worker.worker_name = model.worker_name;
            worker.worker_status = model.worker_status;
            worker.salary = model.salary;
            db.SaveChanges();
            db.Workers.Add(model);
            return RedirectToAction("EmployeeList");
        }

        //Delete Employee
        public ActionResult DeleteEmployee(int? id)
        {
            var worker = db.Workers.Single(x => x.id == id);
            return View(worker);
        }
        [HttpPost, ActionName("DeleteEmployee")]
        public ActionResult DeleteEmployee(int id)
        {
            var worker = db.Workers.SingleOrDefault(x => x.id == id);
            db.Workers.Remove(worker);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }
        //Employee Details
        public ActionResult EmployeeDetails(int id)
        {
            var worker = db.Workers.SingleOrDefault(x => x.id == id);
            return View(worker);
        }

        //Send Order
        public ActionResult SendOrder(int id)
        {
            return View();
        }
    }
}
