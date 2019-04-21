using System.Web.Mvc;
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

        
    }
}
