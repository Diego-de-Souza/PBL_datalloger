namespace AtmoTrack_web_page.Models
{
    public class PadraoViewModel
    {
        public virtual int Id { get; set; }

        public virtual int EstadoId { get; set; }
        public virtual int CidadeId { get; set; }
        public virtual int EmpresaId { get; set; }

        public virtual DateTime DataRegistro { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
    }
}
