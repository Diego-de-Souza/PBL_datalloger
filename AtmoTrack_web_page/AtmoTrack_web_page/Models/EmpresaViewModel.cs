namespace AtmoTrack_web_page.Models
{
    public class EmpresaViewModel: PadraoViewModel
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ {  get; set; }
        public string InscricaoEstadual { get; set; }
        public string WebSite { get; set; }
        public string Telefone1 { get; set; }
        public string? Telefone2 { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Tipo { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime? DataAlteracao { get; set; }



    }
}
