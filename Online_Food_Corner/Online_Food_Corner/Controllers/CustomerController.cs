using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Online_Food_Corner.Models;

namespace Online_Food_Corner.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ApplicationDbContext db;
        //Constructor
        public CustomerController()
        {
            db = new ApplicationDbContext();
        }

        //Destructor
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
        public ActionResult UserProfile()
        {
            string userId = User.Identity.GetUserId();
            var user = (new ApplicationDbContext()).Users.FirstOrDefault(s => s.Id == userId);
            return View(user);
        }
        //User Dashboard
        public ActionResult CustomerDashboard()
        {
            
            var products = db.Products.ToList();
            return View(products);
        }
        //Product List Read Only
        public ActionResult ProductListReadOnly(string name)
        {
            if (name.IsNullOrWhiteSpace())
            {
                List<Product> products = db.Products.ToList();
                return View(products);
            }
            else
            {
                return View((db.Products.Where(p => p.product_name.Contains(name))).ToList());
            }
            
        }
        
        //Make Order
        public ActionResult MakeOrder(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakeOrder(int id, Order model)
        {
            if (!ModelState.IsValid)
            {
                var order = new Order();
                order = model;
                return View("MakeOrder", order);
            }
            else
            {
                var d = User.Identity.GetUserId();
                var product = db.Products.SingleOrDefault(x => x.id == id);
                model.Product = product;
                var customer = db.Customers.SingleOrDefault(x => x.ApplicationUserId == d);
                model.Customer = customer;
                model.timing = DateTime.Now;
                model.order_date = DateTime.Now;
                model.Status = "Not Delivered";
                model.total_price = (product.product_Price / product.product_quantity) * model.Quantity;
                db.Orders.Add(model);
                db.SaveChanges();
                return RedirectToAction("OrderList");
            }
        }
        //Order List
        public ActionResult OrderList()
        {
            var d = User.Identity.GetUserId();
            var orders = db.Orders.Where(x=>x.Customer.ApplicationUserId == d);
            return View(orders.ToList());
        }
        //Make Order List Form
        public ActionResult MakeOrderList()
        {
            ProductViewModel model = new ProductViewModel();
            model.Products = db.Products.ToList<Product>();
            var countProducts = db.Products.ToList().Count;
            ViewBag.count = countProducts;
            return View(model);
        }
        [HttpPost]
        public ActionResult MakeOrderList(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("MakeOrderList");
            }
            else
            {
                var selectedProducts = new List<Product>();
                foreach (var product in model.Products)
                {
                    if (product.IsChecked == true)
                    {
                        selectedProducts.Add(product);
                    }

                }

                var d = User.Identity.GetUserId();

                var order = new Order();
                var customer = db.Customers.SingleOrDefault(x => x.ApplicationUserId == d);
                foreach (var item in selectedProducts)
                {
                    var product = db.Products.SingleOrDefault(x => x.id == item.id);

                    order.Product = product;
                    order.Customer = customer;
                    order.timing = DateTime.Now;
                    order.order_date = DateTime.Now;
                    order.total_price = product.product_Price / product.product_quantity; ;
                    order.Quantity = 1;
                    order.delivery_mode = "Normal";
                    order.Status = "Not Delivered";
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
                return RedirectToAction("OrderList");
            }
            
        }
        //Edit Order
        public ActionResult EditOrder(int id)
        {
            var d = User.Identity.GetUserId();
            var order = db.Orders.SingleOrDefault(o => o.id == id && o.Customer.ApplicationUserId == d);
            return View(order);
        }
        [HttpPost]
        public ActionResult EditOrder(int id, Product model)
        {
            if (!ModelState.IsValid)
            {
                var ordr = new Order();
                ordr.Product = model;
                return View("EditOrder", ordr);
            }
            else
            {
                var d = User.Identity.GetUserId();
                var order = db.Orders.SingleOrDefault(o => o.id == id && o.Customer.ApplicationUserId == d);
                var product = db.Products.SingleOrDefault(x => x.id == model.id);
                order.Product = product;
                order.order_date = DateTime.Now;
                order.timing = DateTime.Now;
                order.total_price = product.product_Price / product.product_quantity;
                db.SaveChanges();
                return RedirectToAction("OrderList");
            }
            
        }
        //Delete Order
        public ActionResult DeleteOrder(int id)
        {
            var d = User.Identity.GetUserId();
            var order = db.Orders.SingleOrDefault(o => o.id == id && o.Customer.ApplicationUserId == d);
            return View(order);
        }
        [HttpPost]
        public ActionResult DeleteOrder(int id, Order model)
        {
            var order = db.Orders.Single(o => o.id == id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("OrderList");
        }
    }
}
