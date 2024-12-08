

using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:6001/messages")
    .WithAutomaticReconnect()
    .Build();

connection.Reconnecting += Reconnecting;
connection.Reconnected += Reconnected;

connection.On<string, string>("UpdateAllAsync",
    (user,message) => {
        Console.WriteLine($"Message SERVER: {user} - {message}");
    });

connection.On<List<string>>("broadcasttoclient",
    (message) => {
        Console.WriteLine($"Message CLIENTS: {string.Join("-", message)}");
    });


await connection.StartAsync();

Console.WriteLine("Connection started.");

while (true)
{
    Console.WriteLine("Escriu un texte: ");
    var message = Console.ReadLine();
    List<string> datosEnviar = new();
    datosEnviar.Add("CONSOLA");
    datosEnviar.Add(message);
    await connection.InvokeAsync("BroadcastToConnection", datosEnviar, connection.ConnectionId);
   
}

Task Reconnecting(Exception arg)
{
   Console.WriteLine("Attempting to reconnect...");
   return Task.CompletedTask;
}

Task Reconnected(string arg)
{
    Console.WriteLine("Connection restored.");
    return Task.CompletedTask;
}