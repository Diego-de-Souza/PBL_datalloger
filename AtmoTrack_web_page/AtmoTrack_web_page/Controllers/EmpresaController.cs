using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AtmoTrack_web_page.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                EmpresaDAO dao = new EmpresaDAO();
                var em = dao.Listagem();

                return View(em);
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
                ViewBag.cadastroEmpresa = "I";
                EmpresaDAO dao = new EmpresaDAO();
                EmpresaViewModel em = new EmpresaViewModel();
                em.Id = dao.LastId();

                var estados = dao.GetAllStates().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Estado
                }).ToList();

                ViewBag.Estados = estados;

                return View("Form", em);
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

        public IActionResult GetCidades(int estadoId)
        {
            try
            {
                EmpresaDAO dao = new EmpresaDAO();
                var cidades = dao.GetAllCitiesEstadoId(estadoId);
                return Json(cidades);
            }
            catch (Exception erro)
            {
                return Json(new { error = erro.Message });
            }
        }
        public IActionResult Salvar(EmpresaViewModel em, string cadastroEmpresa)
        {
            try
            {
                ValidaDados(em, cadastroEmpresa);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = cadastroEmpresa;

                    // Repopula os estados
                    EmpresaDAO dao = new EmpresaDAO();
                    var estados = dao.GetAllStates().Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Estado
                    }).ToList();

                    ViewBag.Estados = estados;

                    ViewBag.cadastroEmpresa = cadastroEmpresa;

                    return View("Form", em);
                }
                else
                {
                    var dao = new EmpresaDAO();
                    if (cadastroEmpresa == "I")
                    {
                        dao.Inserir(em);
                    }
                    else if (cadastroEmpresa == "A")
                    {
                        dao.Alterar(em);
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                return View("Error", erro.ToString());
            }
        }

        public IActionResult Editar(int id)
        {
            try
            {
                ViewBag.cadastroEmpresa = "A";
                EmpresaDAO dao = new EmpresaDAO();
                EmpresaViewModel em = dao.Consulta(id);
                var estados = dao.GetAllStates().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Estado
                }).ToList();

                ViewBag.Estados = estados;

                return View("Form", em);
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
                EmpresaDAO dao = new EmpresaDAO();
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
                EmpresaDAO dao = new EmpresaDAO();
                var em = dao.Consulta(id);
                if (em == null)
                {
                    return NotFound();
                }

                var estado = dao.ConsultaEstado(em.EstadoId);

                ViewBag.EstadoNome = estado != null ? estado.Estado : "Estado não encontrado";

                if (estado == null)
                {
                    return NotFound();
                }

                var cidade = dao.ConsultaCidade(em.CidadeId);
                ViewBag.CidadeNome = cidade != null ? cidade.Cidade : "Cidade não encontrado";

                return View("ExibirEmpresa", em);
            }
            catch (Exception erro)
            {
                return View("Erro", erro.ToString());
            }
        }

        private void ValidaDados(EmpresaViewModel empresa, string operacao)
        {
            ModelState.Clear(); // Limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês) 
            EmpresaDAO dao = new EmpresaDAO();


            if (empresa.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && dao.Consulta(empresa.Id) != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && dao.Consulta(empresa.Id) == null)
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

            if (empresa.EstadoId <= 0)
                ModelState.AddModelError("EstadoId", "Selecione um estado válido.");

            if (empresa.CidadeId <= 0)
                ModelState.AddModelError("CidadeId", "Selecione uma cidade válida.");

            if (string.IsNullOrEmpty(empresa.Tipo))
                ModelState.AddModelError("Tipo", "Preencha o tipo.");

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
    }
}
