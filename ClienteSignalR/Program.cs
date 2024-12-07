

using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:6001/messages")
    .WithAutomaticReconnect()
    .Build();

connection.On<string, string>("UpdateAllAsync1",
    (user,message) => {
        Console.WriteLine($"Message SERVER: {user} - {message}");
    });

connection.On<List<string>>("enviardatosdemo",
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
    await connection.InvokeAsync("EnviaDatosDemo", datosEnviar);
   
}

