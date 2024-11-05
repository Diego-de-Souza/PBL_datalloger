using Microsoft.AspNetCore.SignalR;

namespace AtmoTrack_web_page.DAO
{
    public class MQTTHub : Hub
    {
        public async Task SendData(string data)
        {
            await Clients.All.SendAsync("ReceiveData", data);
        }
    }
}
