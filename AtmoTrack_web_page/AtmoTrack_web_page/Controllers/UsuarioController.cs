using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AtmoTrack_web_page.Controllers
{
    public class UsuarioController : PadraoController<UsuarioViewModel>
    {
        public UsuarioController()
        {
            DAO = new UsuarioDAO();
            GeraProximoId = true;
            TipoRegistro = "U";
        }

        public override void ValidaDados(UsuarioViewModel usuario, string operacao, string statusId)
        {
            ModelState.Clear(); // Limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês) 
            UsuarioDAO dao = new UsuarioDAO();


            if (usuario.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && statusId != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && statusId == null)
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
        /*public IActionResult ExibeConsultaAvancada()
        {
            try
            {
                UsuarioBuscaAvancadaViewModel usuarioBusca = new UsuarioBuscaAvancadaViewModel();

                UsuarioDAO usuarioDAO = new UsuarioDAO();
                var listaUsuarios = usuarioDAO.Listagem();
                EmpresaDAO empresaDAO = new EmpresaDAO();
                var listaEmpresas = empresaDAO.Listagem();
                var listaBusca = listaUsuarios.Select(usuario => new UsuarioBuscaAvancadaViewModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    NomeFantasia = listaEmpresas
                        .Where(empresa => empresa.Id == usuario.EmpresaId)
                        .Select(empresa => empresa.NomeFantasia)
                        .FirstOrDefault(),
                    Estado = listaEmpresas
                        .Where(empresa => empresa.Id == usuario.EmpresaId)
                        .Select(empresa => empresa.Estado)
                        .FirstOrDefault(),
                    DataRegistro = usuario.DataRegistro
                }).ToList();

                return View("BuscaAvancada", listaBusca);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }*/
        /*public IActionResult ObtemDadosConsultaAvancada(int id, string nome, string email, string estados, DateTime dataregistro, string nomeFantasia)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();

                if (string.IsNullOrEmpty(nome))
                    nome = "";
                if (string.IsNullOrEmpty(email))
                    email = "";
                if (string.IsNullOrEmpty(estados))
                    estados = "";
                if (dataregistro.Date == Convert.ToDateTime("01/01/0001"))
                    dataregistro = SqlDateTime.MinValue.Value;
                if (string.IsNullOrEmpty(nomeFantasia))
                    nomeFantasia = "";

                var lista = dao.ConsultaAvancadaUsuario(id, nome, email, estados, dataregistro, nomeFantasia);

                return PartialView("pvGridUs", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }*/

    }
}
