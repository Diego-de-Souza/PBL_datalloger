using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet;
using System.Text;
using AtmoTrack_web_page.DAO;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using MQTTnet.Client.Receiving;

public class MQTTService
{
    private IMqttClient _client;
    private IMqttClientOptions _options;
    private readonly IHubContext<MQTTHub> _hubContext;
    private readonly ConcurrentQueue<string> _receivedMessages = new ConcurrentQueue<string>();


    public MQTTService(IHubContext<MQTTHub> hubContext)
    {
        var factory = new MqttFactory();
        _client = factory.CreateMqttClient();
        _options = new MqttClientOptionsBuilder()
            .WithClientId("fiware_003x")
            .WithTcpServer("20.206.144.151", 1883)
            .WithCleanSession()
            .Build();
        _hubContext = hubContext;

        // Evento para processar mensagens MQTT recebidas
        _client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(async e =>
        {
            var payload = System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            _receivedMessages.Enqueue(payload);

            // Envia o payload para todos os clientes conectados ao SignalR Hub
            await _hubContext.Clients.All.SendAsync("ReceiveData", payload);
        });
    }

    public async Task ConnectAsync()
    {
        if (!_client.IsConnected)
        {
            await _client.ConnectAsync(_options);
            await _client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("/TEF/lamp003x/cmd").Build());
        }
    }

    public async Task DisconnectAsync()
    {
        if (_client.IsConnected)
        {
            await _client.DisconnectAsync();
        }
    }

    // Método para obter mensagens recebidas
    public string[] GetReceivedMessages()
    {
        return _receivedMessages.ToArray();
    }
}
