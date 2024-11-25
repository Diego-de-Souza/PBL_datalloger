using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AtmoTrack_web_page.Controllers
{
    public class EmpresaController : PadraoController<EmpresaViewModel>
    {
        public EmpresaController()
        {
            DAO = new EmpresaDAO();
            GeraProximoId = true;
            TipoRegistro = "E";
        }

        public override void ValidaDados(EmpresaViewModel empresa, string operacao, string statusId)
        {
            ModelState.Clear(); // Limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês) 
            EmpresaDAO dao = new EmpresaDAO();


            if (empresa.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && statusId != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && statusId == null)
                    ModelState.AddModelError("Id", "Usuário não existe.");
            }

            if (string.IsNullOrEmpty(empresa.RazaoSocial))
                ModelState.AddModelError("RazaoSocial", "Preencha a razão social.");

            if (string.IsNullOrEmpty(empresa.NomeFantasia))

                ModelState.AddModelError("NomeFantasia", "Preencha o nome fantasia da empresa.");


            if (string.IsNullOrEmpty(empresa.CNPJ))
            {
                ModelState.AddModelError("CNPJ", "Preencha o CNPJ.");
            }
            else if (!Regex.IsMatch(empresa.CNPJ, @"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$"))
            {
                ModelState.AddModelError("CNPJ", "CNPJ inválido. O formato correto é: 00.000.000/0000-00.");
            }

            if (string.IsNullOrEmpty(empresa.InscricaoEstadual))
                ModelState.AddModelError("InscricaoEstadual", "Preencha a inscrição estadual.");

            if (string.IsNullOrEmpty(empresa.Endereco))
                ModelState.AddModelError("Endereco", "Preencha o endereço.");

            if (string.IsNullOrEmpty(empresa.Estado))
                ModelState.AddModelError("Estado", "Selecione um estado válido.");

            if (string.IsNullOrEmpty(empresa.Cidade))
                ModelState.AddModelError("Cidade", "Selecione uma cidade válida.");

            if (string.IsNullOrEmpty(empresa.NomeService))
                ModelState.AddModelError("Tipo", "Preencha o nome da service.");

            if (string.IsNullOrEmpty(empresa.Cep))
            {
                ModelState.AddModelError("CEP", "Preencha o CEP.");
            }
            else if (!Regex.IsMatch(empresa.Cep, @"^\d{5}-\d{3}$"))
            {
                ModelState.AddModelError("CEP", "CEP inválido. O formato correto é: 00000-000.");
            }

            if (string.IsNullOrEmpty(empresa.WebSite))
            {
                ModelState.AddModelError("WebSite", "Preencha o WebSite.");
            }
            else if (!Regex.IsMatch(empresa.WebSite, @"^(https?:\/\/|www\.)[^\s$.?#].[^\s]*$"))
            {
                ModelState.AddModelError("WebSite", "WebSite inválido. O formato correto é: http://www.exemplo.com, https://www.exemplo.com ou www.exemplo.com.");
            }


            if (string.IsNullOrEmpty(empresa.Telefone1))
            {
                ModelState.AddModelError("Telefone1", "Preencha o telefone1.");
            }
            else if (!Regex.IsMatch(empresa.Telefone1, @"^\(\d{2}\)\d{5}-\d{4}$"))
            {
                ModelState.AddModelError("Telefone1", "Telefone1 inválido. O formato correto é: (00)00000-0000.");
            }

            if (!string.IsNullOrEmpty(empresa.Telefone2))
            {

                if (!Regex.IsMatch(empresa.Telefone2, @"^\(\d{2}\)\d{5}-\d{4}$"))
                {
                    ModelState.AddModelError("Telefone", "Telefone inválido. O formato correto é: (00)00000-0000.");
                }
            }


            if (empresa.DataRegistro == DateTime.MinValue)
            {
                ModelState.AddModelError("DataRegistro", "Data de registro inválida.");
            }
            else
            {
                DateTime minDate = new DateTime(1900, 1, 1);
                DateTime maxDate = DateTime.Now;

                if (empresa.DataRegistro < minDate || empresa.DataRegistro > maxDate)
                {
                    ModelState.AddModelError("DataRegistro", "Data de registro fora do intervalo permitido.");
                }
            }
        }

        public IActionResult TesteCep()
        {
            return View();
        }

        public IActionResult ExibeConsultaAvancada()
        {
            try
            {
                EmpresaBuscaAvancadaViewModel EmBusca = new EmpresaBuscaAvancadaViewModel();

                var listaEmpresas = DAO.Listagem();
                EquipamentoDAO equipamentoDAO = new EquipamentoDAO();
                var listaEquipamentos = equipamentoDAO.Listagem();
                var listaBusca = listaEmpresas.Select(emp => new EmpresaBuscaAvancadaViewModel
                {
                    Id = emp.Id,
                    NomeFantasia = emp.NomeFantasia,
                    Estado = emp.Estado,
                    DataRegistro = emp.DataRegistro,
                    ConnectionStatus = listaEquipamentos
                        .Where(eq => eq.EmpresaId == emp.Id)
                        .Select(eq => eq.ConnectionStatus)
                        .FirstOrDefault()
                }).ToList();

                return View("BuscaAvancada", listaBusca);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel (erro.Message));
            }
        }

        public IActionResult ObtemDadosConsultaAvancada(int id, string nome, string estados, DateTime dataregistro, string connectionstatus)
        {
            try
            {
                EmpresaDAO dao = new EmpresaDAO();
                if (string.IsNullOrEmpty(nome))
                    nome = "";
                if (string.IsNullOrEmpty(estados))
                    estados = "";
                if (dataregistro.Date == Convert.ToDateTime(dataregistro))
                    dataregistro = SqlDateTime.MinValue.Value;
                if (string.IsNullOrEmpty(connectionstatus))
                    connectionstatus = "";
                var lista = dao.ConsultaAvancadaEmpresa(id, nome, estados, dataregistro, connectionstatus);
                return PartialView("pvGridEm", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }
    }
}
