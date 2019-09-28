using System.Diagnostics;
using AspNetCore_NotifyBalloon.Extensions;
using AspNetCore_NotifyBalloon.Extensions.Alerts;
using Microsoft.AspNetCore.Mvc;
using AspNetCore_NotifyBalloon.Models;

namespace AspNetCore_NotifyBalloon.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View().WithSuccess("It worked!", "You were able to view the about page, congrats!");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GoHome()
        {
            //return RedirectToAction("Index").WithWarning("You were redirected!", "The action you hit has bounced you back to Index!");
            return RedirectToAction("Index").WithSuccess("You were redirected!", "The action you hit has bounced you back to Index!");
        }

        public IActionResult ApiDemo()
        {
            return View();
        }
    }
}
