using System.Web.Mvc;

namespace Natific.Ui.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Natific Task.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Natific Task.";

            return View();
        }
    }
}