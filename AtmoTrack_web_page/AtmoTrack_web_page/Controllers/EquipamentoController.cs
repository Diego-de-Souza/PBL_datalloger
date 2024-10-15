using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace AtmoTrack_web_page.Controllers
{
    public class EquipamentoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                EquipamentoDAO dao = new EquipamentoDAO();
                var eq = dao.Listagem();


                return View(eq);
            }
            catch (Exception erro)
            {

            }
            return View();
        }

        public IActionResult NovoRegistro()
        {
            try
            {
                ViewBag.cadastroEquipamento = "I";
                EquipamentoDAO dao = new EquipamentoDAO();
                EquipamentoViewModel eq = new EquipamentoViewModel();
                eq.Id = dao.LastId();

                var empresas = dao.GetAllEmpresas().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.NomeFantasia
                }).ToList();

                ViewBag.Empresas = empresas;

                return View("Form", eq);
            }
            catch (Exception erro)
            {
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMessage = erro.Message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorViewModel);
            }
        }

        public IActionResult Salvar(EquipamentoViewModel em, string cadastroEquipamento)
        {
            try
            {
                ValidaDados(em, cadastroEquipamento);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = cadastroEquipamento;

                    // Repopula as empresas
                    EquipamentoDAO dao = new EquipamentoDAO();
                    var empresas = dao.GetAllEmpresas().Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.NomeFantasia
                    }).ToList();

                    ViewBag.Empresas = empresas;

                    ViewBag.cadastroEquipamento = cadastroEquipamento;

                    return View("Form", em);
                }
                else
                {
                    var dao = new EquipamentoDAO();
                    if (cadastroEquipamento == "I")
                    {
                        dao.Inserir(em);
                    }
                    else if (cadastroEquipamento == "A")
                    {
                        dao.Alterar(em);
                    }

                    return RedirectToAction("Index");
                }
            }
            //catch (SqlException sqlEx)
            //{
            //    // Log or print the specific SQL error message
            //    //Console.WriteLine(sqlEx.Message);  // ou use um logger apropriado
            //    //var errorViewModel = new ErrorViewModel
            //    //{
            //    //    ErrorMessage = sqlEx.Message,
            //    //    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            //    //};
            //    //return View("Error", errorViewModel);
            //}
            catch (Exception erro)
            {
                //var errorViewModel = new ErrorViewModel
                //{
                //    ErrorMessage = erro.Message,
                //    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                //};
                //Console.WriteLine(erro.Message);
                //return View("Error", errorViewModel);
                return View("Error", erro.ToString());
            }
        }

        public IActionResult Editar(int id)
        {
            try
            {
                ViewBag.cadastroEquipamento = "A";
                EquipamentoDAO dao = new EquipamentoDAO();
                EquipamentoViewModel eq = dao.Consulta(id);
                var empresas = dao.GetAllEmpresas().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.NomeFantasia
                }).ToList();

                ViewBag.Empresas = empresas;

                return View("Form", eq);
            }
            catch (Exception erro)
            {
                return View("Erro", erro.ToString());
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                EquipamentoDAO dao = new EquipamentoDAO();
                dao.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Erro", erro.ToString());
            }
        }

        public IActionResult Exibir(int id)
        {
            try
            {
                EquipamentoDAO dao = new EquipamentoDAO();
                var eq = dao.Consulta(id);
                if (eq == null)
                {
                    return NotFound();
                }

                var empresa = dao.ConsultaEmpresa(eq.EmpresaId);

                ViewBag.EmpresaNome = empresa != null ? empresa.NomeFantasia : "Empresa não encontrado";

                return View("ExibirEmpresa", eq);
            }
            catch (Exception erro)
            {
                return View("Erro", erro.ToString());
            }
        }

        private void ValidaDados(EquipamentoViewModel equipamento, string operacao)
        {
            ModelState.Clear(); // Limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês) 
            EquipamentoDAO dao = new EquipamentoDAO();


            if (equipamento.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && dao.Consulta(equipamento.Id) != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && dao.Consulta(equipamento.Id) == null)
                    ModelState.AddModelError("Id", "Usuário não existe.");
            }

            if (string.IsNullOrEmpty(equipamento.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");

            if (string.IsNullOrEmpty(equipamento.MacAddress))
                ModelState.AddModelError("MacAddress", "Preencha o MacAddress.");

            if (string.IsNullOrEmpty(equipamento.IpAddress))
                ModelState.AddModelError("IpAddress", "Preencha o IpAdress.");

            if (equipamento.EmpresaId <= 0)
                ModelState.AddModelError("EmpresaId", "Selecione uma empresa.");

            if (string.IsNullOrEmpty(equipamento.SSID))
                ModelState.AddModelError("SSID", "Preencha o SSID.");

            if (equipamento.SignalStrength <= 0)
                ModelState.AddModelError("SignalStrength", "Selecione força do sinal.");

            if (string.IsNullOrEmpty(equipamento.ConnectionStatus))
                ModelState.AddModelError("ConnectionStatus", "Preencha o status da conexão.");

            if (string.IsNullOrEmpty(equipamento.SensorData))
                ModelState.AddModelError("SensorData", "Preencha os dados do sensor.");

            if (string.IsNullOrEmpty(equipamento.StatusEquipamento))
                ModelState.AddModelError("StatusEquipamento", "Preencha o status do equipamento.");

            if (string.IsNullOrEmpty(equipamento.AuthToken))
                ModelState.AddModelError("AuthToken", "Preencha o token de autenticação.");

            if (string.IsNullOrEmpty(equipamento.FirmwareVersion))
                ModelState.AddModelError("FirmwareVersion", "Preencha a versão do firmware.");

            if (equipamento.LastUpdate == DateTime.MinValue)
            {
                ModelState.AddModelError("LastUpdate", "Data de atualização inválida.");
            }
            else
            {
                DateTime minDate = new DateTime(1900, 1, 1); // Exemplo de data mínima aceita pelo banco de dados
                DateTime maxDate = DateTime.Now;

                if (equipamento.LastUpdate < minDate || equipamento.LastUpdate > maxDate)
                {
                    ModelState.AddModelError("LastUpdate", "Data de atualização fora do intervalo permitido.");
                }
            }
        }
    }
}
