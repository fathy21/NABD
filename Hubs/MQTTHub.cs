using Microsoft.AspNetCore.SignalR;

namespace NABD.Hubs
{
    public class MQTTHub : Hub
    {
        public async Task SendMessage(string topic, string message, int SpO2, int HR, float Temp)
        {
            await Clients.All.SendAsync("ReceiveMessage", topic, message, SpO2, HR, Temp);
        }
    }
}
