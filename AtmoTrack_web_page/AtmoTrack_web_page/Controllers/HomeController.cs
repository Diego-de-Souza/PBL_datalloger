using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AtmoTrack_web_page.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult RedirectToProperPage(int dashboard)
        {
            Console.WriteLine($"Dashboard: {dashboard}");

            if (HttpContext.Session.GetString("LoggedUser") != null)
            {
                if (dashboard == 1)
                {
                    return RedirectToAction("Dashboard1", "Dashboard");
                }
                else if (dashboard == 2)
                {
                    return RedirectToAction("Dashboard2", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
