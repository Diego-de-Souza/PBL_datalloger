using System.Text;

namespace AtmoTrack_web_page.DAO
{
    public class DashboardDAO
    {
        private readonly HttpClient _client;

        public DashboardDAO()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetLuminosityDataAsync(string data)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://20.206.144.151:8666/STH/v1/contextEntities/type/Lamp/id/urn:ngsi-ld:Lamp:003y/attributes/"+data+"?lastN=30");
            request.Headers.Add("fiware-service", "smart");
            request.Headers.Add("fiware-servicepath", "/");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // Método para acender a lâmpada
        public async Task<string> SetLampStateAsync(string state)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "http://20.206.144.151:1026/v2/entities/urn:ngsi-ld:Lamp:003y/attrs");
            request.Headers.Add("fiware-service", "smart");
            request.Headers.Add("fiware-servicepath", "/");

            // Define o JSON dinamicamente com o valor de estado recebido
            var jsonContent = $@"{{
                ""{state}"": {{
                    ""type"": ""command"",
                    ""value"": """"
                }}
            }}";

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
