using AtmoTrack_web_page.DAO;
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

        public IActionResult Dashboard1()
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
                var data = await _dashboardDAO.GetLuminosityDataAsync(dataObject);
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
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        
    }
}
