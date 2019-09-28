using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bim.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Passaia App Running.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Go Ahead.";

            return View();
        }
    }
}