using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace Budgeting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // just for info, in case needed in the future:
            //var userId = User.Identity.GetUserId();
            //var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = manager.FindById(userId);

            return View();
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