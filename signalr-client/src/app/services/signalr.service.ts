import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';



@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;
  private connectionId: string = '';
  messages: { user: string; message: string }[] = [];

  constructor() {}

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:6001/messages') // Cambia la URL según tu servidor
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Conexión SignalR establecida'))
      .then(() => this.getConnectionId())
      .catch((err) => console.error('Error al conectar con SignalR: ', err));

      this.hubConnection.onreconnected(() => {
        console.log("restableciendo conexión");
      })
  }

  private getConnectionId = () => {
    this.hubConnection.invoke('getconnectionid')
    .then((data) => {
      console.log("Connection Id : ", data);
      this.connectionId = data;
    });
  }

  public addMessageListener(): void {
    this.hubConnection.on('UpdateAllAsync', (user: string, message: string) => {
      console.log('Mensaje usuario recibido:', { user, message });
      this.messages.push({ user, message });
    });
  }

  public sendMessage(message: string[]): void {
    this.hubConnection.invoke('BroadcastToConnection', message, this.connectionId)
        .catch((err) => {
          console.error('Error al enviar mensaje: ', err);
    });
  }

  public addSendMessageListener = () => {
    this.hubConnection.on('broadcasttoclient', (data) => {
      console.log("Datos recibidos: ", data);
    })
  }
}
