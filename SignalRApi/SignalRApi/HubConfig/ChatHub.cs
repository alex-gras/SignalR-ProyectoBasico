using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.HubConfig
{
    public class ChatHub : Hub
    {
        public async Task BroadcastToConnection(List<string> data, string connectionId) =>
               await Clients.Client(connectionId).SendAsync("broadcasttoclient", data);

        public string GetConnectionId() => Context.ConnectionId;
    }

}
