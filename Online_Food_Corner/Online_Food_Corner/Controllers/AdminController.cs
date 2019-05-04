using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Online_Food_Corner.Models;

namespace Online_Food_Corner.Controllers
{
    [Authorize]
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
        //UserManager
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //Profile
        [Authorize(Roles = RoleName.CanManageOFC + "," + RoleName.Chef + "," + RoleName.DeliveryTeamMember)]
        public ActionResult UserProfile()
        {
            string userId = User.Identity.GetUserId();
            var user = (new ApplicationDbContext()).Users.FirstOrDefault(s => s.Id == userId);
            return View(user);
        }

        // Admin Dashboard
        [Authorize(Roles = RoleName.CanManageOFC)]
        public ActionResult AdminDashboard()
        {
            return View();
        }
        [Authorize(Roles = RoleName.Chef + "," + RoleName.CanManageOFC)]
        public ActionResult ChefDashboard()
        {
            return View();
        }
        [Authorize(Roles = RoleName.DeliveryTeamMember + "," + RoleName.CanManageOFC)]
        public ActionResult TeamMemberDashboard()
        {
            return View();
        }

        //EMenu
        [AllowAnonymous]
        public ActionResult Menu()
        {
            var products = db.Products.ToList();

            return View(products);
        }

        //Product List
        [Authorize(Roles = RoleName.CanManageOFC)]
        public ActionResult ProductList()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        //Add Product
        [Authorize(Roles = RoleName.CanManageOFC)]
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [Authorize(Roles = RoleName.CanManageOFC)]
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
        [Authorize(Roles = RoleName.CanManageOFC)]
        public ActionResult ProductDetails(int id)
        {
            var product = db.Products.SingleOrDefault(x => x.id == id);
            return View(product);
        }

        //Delete Product
        [Authorize(Roles = RoleName.CanManageOFC)]
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
        [Authorize(Roles = RoleName.CanManageOFC)]
        public ActionResult OrderList()
        {
            var order = db.Orders.ToList();
            ViewBag.count = order.Count;
            return View(order);
        }
        //Order Details
        [Authorize(Roles = RoleName.CanManageOFC)]
        public ActionResult OrderDetails(int id)
        {
            var order = db.Orders.SingleOrDefault(x => x.id == id);
            return View(order);
        }
        //Create employee
        [Authorize(Roles = RoleName.CanManageOFC)]
        public ActionResult CreateEmployee()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Worker model)
        {
            if (!ModelState.IsValid)
            {
                var worker = new Worker();
                worker = model;
                return View("CreateEmployee", worker);
            }
            else
            {
                var user = new ApplicationUser();
                user.UserName = model.Email;
                user.firstName = model.worker_name;
                user.address = model.Address;
                user.cellNumber = model.CellNumber;
                user.PasswordHash = System.Web.Helpers.Crypto.HashPassword(model.Password) ;
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.Email = model.Email;
                db.Users.Add(user);
                model.ApplicationUserId = user.Id;
                db.Workers.Add(model);
                db.SaveChanges();
                if (model.worker_status == "Chef")
                {
                    UserManager.AddToRole(model.ApplicationUserId, "Chef");
                }
                else
                {
                    UserManager.AddToRole(model.ApplicationUserId, "Delivery Team Member");
                }
                return RedirectToAction("EmployeeList");
            }
            
        }
        [Authorize(Roles = RoleName.CanManageOFC)]
        //Employee List
        public ActionResult EmployeeList()
        {
            var employee = db.Workers.ToList();
            return View(employee);
        }
        [Authorize(Roles = RoleName.CanManageOFC)]
        //Edit Employee
        public ActionResult EditEmployee(int id)
        {
            var model = db.Workers.SingleOrDefault(x => x.id == id);
            return View(model);
        } 
        [HttpPost]
        public ActionResult EditEmployee(int id, Worker model)
        {
            if (!ModelState.IsValid)
            {
                var worker = new Worker();
                worker = model;
                return View("CreateEmployee", worker);
            }
            else
            {
                var worker = db.Workers.Single(x => x.id == id);

                worker.worker_name = model.worker_name;
                worker.worker_status = model.worker_status;
                worker.salary = model.salary;

                db.SaveChanges();
                return RedirectToAction("EmployeeList");
            }
            
        }
        [Authorize(Roles = RoleName.CanManageOFC)]
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
        [Authorize(Roles = RoleName.CanManageOFC)]
        //Employee Details
        public ActionResult EmployeeDetails(int id)
        {
            var worker = db.Workers.SingleOrDefault(x => x.id == id);
            return View(worker);
        }
        [Authorize(Roles = RoleName.CanManageOFC)]
        //Customer List
        public ActionResult CustomerList()
        {
            var customer = db.Customers.ToList();
            ViewBag.count = customer.Count;
            return View(customer);
        }
        [Authorize(Roles = RoleName.CanManageOFC)]
        //Send Order
        //OrderId
        public ActionResult SendOrder(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendOrder(int id, WorkerOrder model)
        {
            if (!ModelState.IsValid)
            {
                var employeOrder = new WorkerOrder();
                employeOrder = model;
                return View("SendOrder", employeOrder);
            }
            else
            {
                var order = db.Orders.SingleOrDefault(x => x.id == id);
                var worker = db.Workers.SingleOrDefault(x => x.id == model.WorkerId);
                model.Order = order;
                model.Worker = worker;
                db.WorkerOrders.Add(model);
                db.SaveChanges();
                return RedirectToAction("WorkersOrder");
            }
            
        }
        [Authorize(Roles = RoleName.CanManageOFC)]
        //workers Order List
        public ActionResult WorkersOrder()
        {
            var workersOrder = db.WorkerOrders.ToList();
            return View(workersOrder);
        }
        [Authorize(Roles = RoleName.CanManageOFC)]
        //Edit Worker Order
        public ActionResult EditWorkerOrder(int id)
        {
            var workersOrder = db.WorkerOrders.SingleOrDefault(x=> x.Id == id);
            return View(workersOrder);
        }

        [HttpPost]
        public ActionResult EditWorkerOrder(int id, WorkerOrder model)
        {
            if (!ModelState.IsValid)
            {
                var employeOrder = new WorkerOrder();
                employeOrder = model;
                return View("SendOrder", employeOrder);
            }
            else
            {
                var workersOrder = db.WorkerOrders.Single(x => x.Id == id);
                var worker = db.Workers.Single(x => x.id == model.WorkerId);
                var order = db.Orders.Single(x => x.id == model.OrderId);
                workersOrder.Worker = worker;
                workersOrder.Order = order;
                db.SaveChanges();
                return RedirectToAction("WorkersOrder");
            }
            
        }
        [Authorize(Roles = RoleName.CanManageOFC)]
        //delete worker order
        public ActionResult DeleteWorkerOrder(int? id)
        {
            var workerOrder = db.WorkerOrders.SingleOrDefault(x => x.Id == id);
            return View(workerOrder);
            
        }
        
        [HttpPost]
        public ActionResult DeleteWorkerOrder(int id)
        {
            var workerOrder = db.WorkerOrders.Single(x => x.Id == id);
            db.WorkerOrders.Remove(workerOrder);
            db.SaveChanges();
            return RedirectToAction("WorkersOrder");
        }

        //Employee Order
        [Authorize(Roles = RoleName.CanManageOFC + "," + RoleName.Chef + "," + RoleName.DeliveryTeamMember)]
        public ActionResult EmployeeOrder()
        {
            var d = User.Identity.GetUserId();
            var workerOrder = db.WorkerOrders.Where(x=>x.Worker.ApplicationUserId == d).ToList();
            return View(workerOrder);
        }
        //Send Reply
        [Authorize(Roles = RoleName.DeliveryTeamMember +","+ RoleName.CanManageOFC)]
        public ActionResult SendReply(int id)
        {
            var workerOrder = db.WorkerOrders.SingleOrDefault(x => x.Id == id);
            var order = db.Orders.SingleOrDefault(x => x.id == workerOrder.OrderId);
            return View(order);
        }
        [HttpPost]
        public ActionResult SendReply(int id, Order model)
        {
            var workersOrder = db.WorkerOrders.Single(x => x.Id == id);
            var order = db.Orders.Single(x => x.id == workersOrder.OrderId);
            order.Status = model.Status;

            workersOrder.Order = order;
            db.SaveChanges();
            return RedirectToAction("EmployeeOrder");
            
        }

    }
}
