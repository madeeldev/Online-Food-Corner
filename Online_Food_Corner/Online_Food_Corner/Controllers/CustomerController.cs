using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Online_Food_Corner.Models;

namespace Online_Food_Corner.Controllers
{
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
        //User Dashboard
        public ActionResult CustomerDashboard()
        {
            
            var products = db.Products.ToList();
            return View(products);
        }
        //Product List Read Only
        public ActionResult ProductListReadOnly()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        //Make Order
        public ActionResult MakeOrder(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakeOrder(int id, Order model)
        {
            var d = User.Identity.GetUserId();
            var product = db.Products.SingleOrDefault(x => x.id == id);
            model.Product = product;
            var customer = db.Customers.SingleOrDefault(x => x.ApplicationUserId == d);
            model.Customer = customer;
            model.timing = DateTime.Now;
            model.order_date = DateTime.Now;
            model.total_price = (product.product_Price/product.product_quantity) * model.Quantity;
            db.Orders.Add(model);
            db.SaveChanges();
            return RedirectToAction("OrderList");
        }
        //Order List
        public ActionResult OrderList()
        {
            var d = User.Identity.GetUserId();
            var orders = db.Orders.Where(x=>x.Customer.ApplicationUserId == d);
            return View(orders.ToList());
        }

    }
}
