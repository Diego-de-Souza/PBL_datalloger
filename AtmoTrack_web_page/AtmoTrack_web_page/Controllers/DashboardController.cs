using Microsoft.AspNetCore.Mvc;

namespace AtmoTrack_web_page.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard1()
        {
            // Verifica se o usuário está logado
            if (HttpContext.Session.GetString("LoggedUser") != null)
            {
                return View();
            }
            else
            {
                // Se não estiver logado, redireciona para a página de login
                return RedirectToAction("Index", "Login");
            }
            
        }

        private static readonly Random Random = new Random();

        // Endpoint que fornece dados em JSON
        [HttpGet]
        public IActionResult GetData()
        {
            // Gera dados aleatórios
            var labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul" };
            var values = new int[labels.Length];

            for (int i = 0; i < values.Length; i++)
            {
                // Gera valores aleatórios entre 1 e 100
                values[i] = Random.Next(1, 100);
            }

            var data = new
            {
                Labels = labels,
                Values = values
            };

            return Json(data);
        }
        public IActionResult Dashboard2()
        {
            // Verifica se o usuário está logado
            if (HttpContext.Session.GetString("LoggedUser") != null)
            {
                return View();
            }
            else
            {
                // Se não estiver logado, redireciona para a página de login
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
