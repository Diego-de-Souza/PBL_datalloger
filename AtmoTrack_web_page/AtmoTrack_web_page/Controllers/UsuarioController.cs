using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
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
