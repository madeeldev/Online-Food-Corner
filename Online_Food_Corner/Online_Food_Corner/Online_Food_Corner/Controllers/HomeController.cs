using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Food_Corner.Models;

namespace Online_Food_Corner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var products = db.Products.ToList();
            return View(products);
        }
        [AllowAnonymous]
        //CustomerSearch
        public ActionResult Search(string searchName)
        {
            var db = new ApplicationDbContext();
            if (searchName == null)
            {

                var products = db.Products.ToList();
                return View(products);
            }
            else
            {
                var products = db.Products.Where(p => p.product_name.Contains(searchName)).ToList();
                return View(products);
            }

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}