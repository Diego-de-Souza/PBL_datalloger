namespace AtmoTrack_web_page.Models
{
    public class LogEventoViewModel
    {
        public Int32 Id { get; set; }
        public DateTime DataHora { get; set; }
        public string NomeServiceEvento { get; set; }
        public double Temperatura { get; set; }
    }
}
