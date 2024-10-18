using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AtmoTrack_web_page.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult RedirectToProperPage()
        {
            // Verifica se o usuário está logado
            if (HttpContext.Session.GetString("LoggedUser") != null)
            {
                // Lógica para redirecionar para a página correta
                return RedirectToAction("Dashboard1", "Dashboard");
            }
            else
            {
                // Se não estiver logado, redireciona para a página de login
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
