namespace AtmoTrack_web_page.Models
{
    public class EquipamentoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EmpresaId { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public string SSID { get; set; }
        public int SignalStrength { get; set; } // Em dBm
        public string ConnectionStatus { get; set; }
        public DateTime DataRegistro { get; set; }
        public string SensorData { get; set; }
        public string StatusEquipamento { get; set; }
        public string AuthToken { get; set; }
        public string FirmwareVersion { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
