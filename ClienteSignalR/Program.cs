using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7139/messages")
    .WithAutomaticReconnect()
    .Build();

connection.On<string, string>("UpdateAllAsync",
    (user,message) => {
        Console.WriteLine($"Message received: {user} - {message}");
    });

await connection.StartAsync();
Console.WriteLine("Connection started.");

while (true)
{
    Console.Write("Exit: pulsa una tecla");
    var message = Console.ReadLine();
    //NO FUNCIONA EL ENVÍO
    await connection.InvokeAsync("SendMessage", "CONSOLA", message);
    
}

