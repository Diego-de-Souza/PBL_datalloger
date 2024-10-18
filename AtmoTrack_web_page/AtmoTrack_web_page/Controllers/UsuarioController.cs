using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AtmoTrack_web_page.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                var us = dao.Listagem();

                return View(us);
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
                ViewBag.cadastroUsuario = "I";
                UsuarioDAO dao = new UsuarioDAO();
                UsuarioViewModel us = new UsuarioViewModel();
                us.Id = dao.LastId();

                var estados = dao.GetAllStates().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Estado
                }).ToList();

                ViewBag.Estados = estados;

                return View("Form",us);
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
                UsuarioDAO dao = new UsuarioDAO();
                var cidades = dao.GetAllCitiesEstadoId(estadoId);
                return Json(cidades);
            }
            catch (Exception erro)
            {
                return Json(new { error = erro.Message });
            }
        }

        public IActionResult Salvar(UsuarioViewModel us, string cadastroUsuario)
        {
            try
            {
                ValidaDados(us, cadastroUsuario);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = cadastroUsuario;

                    // Repopula os estados
                    UsuarioDAO dao = new UsuarioDAO();
                    var estados = dao.GetAllStates().Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Estado
                    }).ToList();

                    ViewBag.Estados = estados;

                    ViewBag.cadastroUsuario = cadastroUsuario;

                    // Se um estado foi selecionado, repopula as cidades
                    if (us.EstadoId > 0)
                    {
                        var cidades = dao.GetAllCitiesEstadoId(us.EstadoId).Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Cidade
                        }).ToList();
                        ViewBag.Cidades = cidades;
                    }

                    return View("Form", us);
                }
                else
                {
                    var dao = new UsuarioDAO();
                    if (cadastroUsuario == "I")
                    {
                        dao.Inserir(us);
                    }
                    else if (cadastroUsuario == "A")
                    {
                        dao.Alterar(us);
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
                ViewBag.cadastroUsuario = "A";
                UsuarioDAO dao = new UsuarioDAO();
                UsuarioViewModel us = dao.Consulta(id);
                var estados = dao.GetAllStates().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Estado
                }).ToList();

                ViewBag.Estados = estados;

                return View("Form", us);
            }
            catch(Exception erro)
            {
                return View("Error", erro.ToString());
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                dao.Excluir(id);
                return RedirectToAction("Index");
            }catch (Exception erro)
            {
                return View("Error", erro.ToString());
            }
        }

        public IActionResult Exibir(int id)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                var us = dao.Consulta(id);
                if (us == null)
                {
                    return NotFound();
                }

                var estado = dao.ConsultaEstado(us.EstadoId);

                ViewBag.EstadoNome = estado != null ? estado.Estado : "Estado não encontrado";

                if(estado == null)
                {
                    return NotFound();
                }

                var cidade = dao.ConsultaCidade(us.CidadeId);
                ViewBag.CidadeNome = cidade != null ? cidade.Cidade : "Estado não encontrado";

                return View("VisualizarUsuario", us);
            }catch(Exception erro)
            {
                return View("Error", erro.ToString());
            }
        }

        private void ValidaDados(UsuarioViewModel usuario, string operacao)
        {
            ModelState.Clear(); // Limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês) 
            UsuarioDAO dao = new UsuarioDAO();


            if (usuario.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && dao.Consulta(usuario.Id) != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && dao.Consulta(usuario.Id) == null)
                    ModelState.AddModelError("Id", "Usuário não existe.");
            }

            if (string.IsNullOrEmpty(usuario.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");

            if (string.IsNullOrEmpty(usuario.Email))
            {
                ModelState.AddModelError("Email", "Preencha o email.");
            }
            else if (!Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ModelState.AddModelError("Email", "Email inválido. O formato correto é: exemplo@exemplo.com");
            }

            if (string.IsNullOrEmpty(usuario.Senha))
                ModelState.AddModelError("Senha", "Preencha a senha.");

            if (usuario.Senha != usuario.ConfirmacaoSenha)
                ModelState.AddModelError("ConfirmacaoSenha", "A senha e a confirmação de senha não coincidem.");

            if (string.IsNullOrEmpty(usuario.Endereco))
                ModelState.AddModelError("Endereco", "Preencha o endereço.");

            if (usuario.EstadoId <= 0)
                ModelState.AddModelError("EstadoId", "Selecione um estado válido.");

            if (usuario.CidadeId <= 0)
                ModelState.AddModelError("CidadeId", "Selecione uma cidade válida.");

            if (string.IsNullOrEmpty(usuario.Cep))
            {
                ModelState.AddModelError("CEP", "Preencha o CEP.");
            }
            else if (!Regex.IsMatch(usuario.Cep, @"^\d{5}-\d{3}$"))
            {
                ModelState.AddModelError("CEP", "CEP inválido. O formato correto é: 00000-000.");
            }

            // Validação de Telefone usando regex
            if (string.IsNullOrEmpty(usuario.Telefone))
            {
                ModelState.AddModelError("Telefone", "Preencha o telefone.");
            }
            else if (!Regex.IsMatch(usuario.Telefone, @"^\(\d{2}\)\d{5}-\d{4}$"))
            {
                ModelState.AddModelError("Telefone", "Telefone inválido. O formato correto é: (00)00000-0000.");
            }

            if (!string.IsNullOrEmpty(usuario.TelefoneComercial)) // Verifica se o telefone não está vazio
            {
                // Validação de Telefone usando regex
                if (!Regex.IsMatch(usuario.TelefoneComercial, @"^\(\d{2}\)\d{5}-\d{4}$"))
                {
                    ModelState.AddModelError("TelefoneComercial", "Telefone comercial inválido. O formato correto é: (xx)xxxxx-xxxx.");
                }
            }

            if (string.IsNullOrEmpty(usuario.Empresa))
                ModelState.AddModelError("Empresa", "Preencha a empresa.");

            if (string.IsNullOrEmpty(usuario.Cargo))
                ModelState.AddModelError("Cargo", "Preencha o cargo.");
        }
    }
}
