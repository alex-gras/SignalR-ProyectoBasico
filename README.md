# signalR - Basico + Angular

**Documentación**
* [.NET Core with SignalR and Angular – Real-Time Charts](https://code-maze.com/netcore-signalr-angular-realtime-charts/)
* [How to Send Client-Specific Messages Using SignalR](https://code-maze.com/how-to-send-client-specific-messages-using-signalr/)
* [Introducción a ASP.NET Core SignalR](https://learn.microsoft.com/es-es/aspnet/core/signalr/introduction?view=aspnetcore-9.0)


## API 
Contiene un endpoint para enviar mensajes

**Controller**
````c#
 [HttpPost("sendNotification")]
 public async Task<IActionResult> SendNotification([FromBody] Notification notification)
 {
     await _hubContext.Clients.All.SendAsync("UpdateAllAsync", notification.User, notification.Message);
     return Ok(new { Message = "Notification sent successfully" });
 }
````

**program.cs**
````c#
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
....
builder.Services.AddSignalR();
....

app.MapHub<ChatHub>("/messages"
    , o => o.AllowStatefulReconnects = true);
````

**Hub**
````c#
 public class ChatHub : Hub
 {
     public async Task SendMessage(string user, string message)
     {
         await Clients.All.SendAsync("UpdateAllAsync", user, message);

     }
````



## Aplicación de consola
Recibe los mensajes de la API y permite enviar mensajes
````c#
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
    await connection.InvokeAsync("SendMessage", "CONSOLA", message);
    
}

````

## Angular
>[!IMPORTANT]  
>Requiere:
>*  **node 20.12.1**
>*  **Angular 19.0.4**

**instalar dependencias**
````bash
npm install
````
**Ejecutar aplicación**
````bash
ng serve -o
````

