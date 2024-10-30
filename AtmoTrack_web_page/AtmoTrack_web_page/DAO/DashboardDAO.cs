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
            var request = new HttpRequestMessage(HttpMethod.Get, "http://20.206.144.151:8666/STH/v1/contextEntities/type/Lamp/id/urn:ngsi-ld:Lamp:003x/attributes/"+data+"?lastN=30");
            request.Headers.Add("fiware-service", "smart");
            request.Headers.Add("fiware-servicepath", "/");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
