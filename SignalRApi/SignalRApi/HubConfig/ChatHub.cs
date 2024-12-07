using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.HubConfig
{
    public class ChatHub : Hub
    {
        public async Task EnviaDatosDemo(List<string> data) =>
               await Clients.All.SendAsync("enviardatosdemo", data);

    }

}
