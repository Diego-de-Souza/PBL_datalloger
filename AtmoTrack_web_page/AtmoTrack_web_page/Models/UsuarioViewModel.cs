using System.ComponentModel.DataAnnotations.Schema;

namespace AtmoTrack_web_page.Models
{
    public class UsuarioViewModel: PadraoViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public string? TelefoneComercial { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Estado { get; set; }

        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Uf {  get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime? DataAlteracao { get; set; }


        [NotMapped] // Essa propriedade não será salva no banco de dados
        public string ConfirmacaoSenha { get; set; }
    }
}
