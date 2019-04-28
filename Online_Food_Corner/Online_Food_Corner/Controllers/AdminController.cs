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

        //EMenu
        public ActionResult Menu()
        {
            var products = db.Products.ToList();

            return View(products);
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
            ViewBag.count = order.Count;
            return View(order);
        }
        //Order Details
        public ActionResult OrderDetails(int id)
        {
            var order = db.Orders.SingleOrDefault(x => x.id == id);
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

        //Customer List
        public ActionResult CustomerList()
        {
            var customer = db.Customers.ToList();
            ViewBag.count = customer.Count;
            return View(customer);
        }
        //Send Order
        //OrderId
        public ActionResult SendOrder(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendOrder(int id, WorkerOrder model)
        {
            var order = db.Orders.SingleOrDefault(x => x.id == id);
            var worker = db.Workers.SingleOrDefault(x=>x.id == model.WorkerId);
            model.Order = order;
            model.Worker = worker;
            db.WorkerOrders.Add(model);
            db.SaveChanges();
            return RedirectToAction("WorkersOrder");
        }
        //workers Order List
        public ActionResult WorkersOrder()
        {
            var workersOrder = db.WorkerOrders.ToList();
            return View(workersOrder);
        }
        //Edit Worker Order
        public ActionResult EditWorkerOrder(int id)
        {
            var workersOrder = db.WorkerOrders.SingleOrDefault(x=> x.Id == id);
            return View(workersOrder);
        }

        [HttpPost]
        public ActionResult EditWorkerOrder(int id, WorkerOrder model)
        {
            var workersOrder = db.WorkerOrders.Single(x=> x.Id == id);
            var worker = db.Workers.Single(x => x.id == model.WorkerId);
            var order = db.Orders.Single(x => x.id == model.OrderId);
            workersOrder.Worker = worker;
            workersOrder.Order = order;
            db.SaveChanges();
            return RedirectToAction("WorkersOrder");
        }
        //delete worker order
        public ActionResult DeleteWorkerOrder(int? id)
        {
            var workerOrder = db.WorkerOrders.SingleOrDefault(x => x.Id == id);
            return View(workerOrder);
            
        }

        //Delete worker order
        [HttpPost]
        public ActionResult DeleteWorkerOrder(int id)
        {
            var workerOrder = db.WorkerOrders.Single(x => x.Id == id);
            db.WorkerOrders.Remove(workerOrder);
            db.SaveChanges();
            return RedirectToAction("WorkersOrder");
        }
    }
}
