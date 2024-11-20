using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AtmoTrack_web_page.Models;
using AtmoTrack_web_page.DAO;

namespace AtmoTrack_web_page.Controllers
{
    public class LoginController : Controller
    {
        private UsuarioDAO _usuarioDAO = new UsuarioDAO();

        // GET: Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dadosUsuario = _usuarioDAO.ConsultaUsuario(model.Login, model.Senha);

                // Verifique se o usuário foi encontrado e as credenciais são válidas
                if (dadosUsuario == "ok")
                {
                    // Definir o valor da sessão
                    HttpContext.Session.SetString("LoggedUser", model.Login);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Login)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    // Redirecionar ao Dashboard após login
                    return RedirectToAction("Index", "Empresa");
                }

                // Se falhar, define a mensagem de erro e retorna à página de login
                ViewBag.ErrorMessage = "As credenciais fornecidas estão incorretas.";
                return View("Index");
            }

            return View(model); // Retorna o modelo caso a validação falhe
        }

        // Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("LoggedUser"); // Remover usuário da sessão
            return RedirectToAction("Index", "Login");
        }
    }
}
