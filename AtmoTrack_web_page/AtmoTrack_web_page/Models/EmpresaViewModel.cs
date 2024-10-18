namespace AtmoTrack_web_page.Models
{
    public class EmpresaViewModel
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ {  get; set; }
        public string InscricaoEstadual { get; set; }
        public string WebSite { get; set; }
        public string Telefone1 { get; set; }
        public string? Telefone2 { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public Int16 EstadoId { get; set; }
        public Int32 CidadeId { get; set; }
        public string Tipo { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime? DataAlteracao { get; set; }


    }
}
