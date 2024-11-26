using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AtmoTrack_web_page.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardDAO _dashboardDAO;

        public DashboardController()
        {
            _dashboardDAO = new DashboardDAO();
        }
        private EmpresaDAO _empresaDAO = new EmpresaDAO();

        public IActionResult Dashboard1(EmpresaViewModel Model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetData(string dataObject)
        {
            try
            {
                var data = await _dashboardDAO.GetTemperatureDataAsync(dataObject);
                return Content(data, "application/json");
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"Erro ao buscar os dados: {e.Message}");
            }
        }

        public async Task<IActionResult> Dashboard2()
        {
            if (User.Identity.IsAuthenticated)
            {
                var listaEmpresas = _empresaDAO.Listagem();
                return View(listaEmpresas);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpPost]
        public async Task<JsonResult> AtivaAlarme(string unidade)
        {
            bool sucesso = true;
            string mensagem = "";
            string lampState = "on";  // Variável para armazenar o estado da lâmpada

            // Definir a mensagem e o estado da lâmpada conforme a unidade
            if (unidade == "temperatura")
            {
                mensagem = $"Alerta! A temperatura está acima do set!";
            }

            var lampadaResposta = await _dashboardDAO.SetLampStateAsync(lampState);

            var mensagemAlert = new { sucesso = sucesso, mensagem = mensagem, lampadaResposta };
            return Json(mensagemAlert);
        }

        [HttpPost]
        public async Task<IActionResult> ApagaLampada()
        {
            string lampState = "off";  
            await _dashboardDAO.SetLampStateAsync(lampState);  

            return NoContent(); 
        }

    }
}
