using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("UpdateAllAsync", user, message);

        }

    }

}
