using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AtmoTrack_web_page.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Exemplo simples com dados fictícios
            if (username == "admin" && password == "password123")
            {
                // Definir o valor da sessão
                HttpContext.Session.SetString("LoggedUser", username);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Redirecionar ao Dashboard após login
                return RedirectToAction("Dashboard1", "Dashboard");
            }

            // Se falhar, define a mensagem de erro e retorna à página de login
            ViewBag.ErrorMessage = "As credenciais fornecidas estão incorretas.";
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("LoggedUser"); // Remover usuário da sessão
            return RedirectToAction("Index", "Login");
        }
    }


}
