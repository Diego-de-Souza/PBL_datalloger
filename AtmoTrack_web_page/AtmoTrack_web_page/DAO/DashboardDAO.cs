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

        //metodo que busca os dados do sensor no sth-comet
        public async Task<string> GetTemperatureDataAsync(string data)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://4.228.58.59:8666/STH/v1/contextEntities/type/Lamp/id/urn:ngsi-ld:Lamp:003t/attributes/" + data+"?lastN=30");
            request.Headers.Add("fiware-service", "nextime");
            request.Headers.Add("fiware-servicepath", "/");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        //metodo que busca dados em tempo real
        public async Task<string> GetTempDataRealTimeAsync(string fiwareService)
        {
            var requestUri = "http://4.228.58.59:1026/v2/entities";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("fiware-service", fiwareService);
            request.Headers.Add("fiware-servicepath", "/");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        // Método para acender ou apagar a lâmpada
        public async Task<string> SetLampStateAsync(string state)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, "http://4.228.58.59:1026/v2/entities/urn:ngsi-ld:Lamp:002t/attrs");
            request.Headers.Add("fiware-service", "nextime");
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

        //provisiona a criação do serviço
        public async Task<string> CreateIoTServiceAsync(string fiwareService)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://4.228.58.59:4041/iot/services");
            request.Headers.Add("fiware-service", fiwareService);
            request.Headers.Add("fiware-servicepath", "/");

            var jsonContent = @"{
                ""services"": [
                    {
                        ""apikey"": ""TEF"",
                        ""cbroker"": ""http://4.228.58.59:1026"",
                        ""entity_type"": ""Thing"",
                        ""resource"": ""@fiwareService""
                    }
                ]
            }";

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        //provisiona o dispositivo para acender a lampada
        public async Task<string> RegisterDeviceAsync(string fiwareService)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://4.228.58.59:4041/iot/devices");
            request.Headers.Add("fiware-service", fiwareService);
            request.Headers.Add("fiware-servicepath", "/");

            var jsonContent = @"{
                ""devices"": [
                    {
                        ""device_id"": ""lamp003t"",
                        ""entity_name"": ""urn:ngsi-ld:Lamp:002t"",
                        ""entity_type"": ""Lamp"",
                        ""protocol"": ""PDI-IoTA-UltraLight"",
                        ""transport"": ""MQTT"",
                        ""commands"": [
                            { ""name"": ""on"", ""type"": ""command"" },
                            { ""name"": ""off"", ""type"": ""command"" }
                        ],
                        ""attributes"": [
                            { ""object_id"": ""s"", ""name"": ""state"", ""type"": ""Text"" },
                            { ""object_id"": ""l"", ""name"": ""temperature"", ""type"": ""Integer"" }
                        ]
                    }
                ]
            }";

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        //provisionando o registro dos comandos para ascender e apagar a lampada
        public async Task<string> RegisterLampCommandsAsync(string fiwareService)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://4.228.58.59:1026/v2/registrations");
            request.Headers.Add("fiware-service", fiwareService);
            request.Headers.Add("fiware-servicepath", "/");

            // Define o conteúdo JSON para a requisição
            var jsonContent = @"{
                ""description"": ""Lamp Commands"",
                ""dataProvided"": {
                    ""entities"": [
                        {
                            ""id"": ""urn:ngsi-ld:Lamp:002t"",
                            ""type"": ""Lamp""
                        }
                    ],
                    ""attrs"": [""on"", ""off""]
                },
                ""provider"": {
                    ""http"": { ""url"": ""http://4.228.58.59:4041"" },
                    ""legacyForwarding"": true
                }
            }";

            // Define o conteúdo da requisição como JSON
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = content;

            // Envia a requisição e verifica se foi bem-sucedida
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        //cria o serviço de notificação pelo sth comet para receber os dados do back-end
        public async Task<string> AddTemperatureSubscriptionAsync(string fiwareService)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://4.228.58.59:1026/v2/subscriptions");
            request.Headers.Add("fiware-service", fiwareService);
            request.Headers.Add("fiware-servicepath", "/");

            // Define o conteúdo JSON para a requisição
            var jsonContent = @"{
                ""description"": ""Notify STH-Comet of all Lamp temperature changes"",
                ""subject"": {
                    ""entities"": [
                        {
                            ""id"": ""urn:ngsi-ld:Lamp:002t"",
                            ""type"": ""Lamp""
                        }
                    ],
                    ""condition"": { ""attrs"": [""temperature""] }
                },
                ""notification"": {
                    ""http"": {
                        ""url"": ""http://4.228.58.59:8666/notify""
                    },
                    ""attrs"": [ ""temperature"" ],
                    ""attrsFormat"": ""legacy""
                }
            }";

            // Define o conteúdo da requisição como JSON
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = content;

            // Envia a requisição e verifica se foi bem-sucedida
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateOrionAsync(string fiwareService)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://4.228.58.59:1026/v2/entities");
            request.Headers.Add("fiware-service", fiwareService);
            request.Headers.Add("fiware-servicepath", "/");

            // Define o conteúdo JSON para a requisição
            var jsonContent = @"{
                                  ""id"": ""urn:ngsi-ld:entity:002t"",
                                  ""type"": ""iot"",
                                  ""temperature"": {
                                  ""type"": ""float"",
                                  ""value"": 0
                                    }
                                }";

            // Define o conteúdo da requisição como JSON
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = content;

            // Envia a requisição e verifica se foi bem-sucedida
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
