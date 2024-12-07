import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';



@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;

  messages: { user: string; message: string }[] = [];

  constructor() {}

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:6001/messages') // Cambia la URL según tu servidor
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Conexión SignalR establecida'))
      .catch((err) => console.error('Error al conectar con SignalR: ', err));
  }

  public addMessageListener(): void {
    this.hubConnection.on('UpdateAllAsync1', (user: string, message: string) => {
      console.log('Mensaje usuario recibido:', { user, message });
      this.messages.push({ user, message });
    });
  }

  public sendMessage(message: string[]): void {
    this.hubConnection.invoke('EnviaDatosDemo', message)
        .catch((err) => {
          console.error('Error al enviar mensaje: ', err);
    });
  }

  public addSendMessageListener = () => {
    this.hubConnection.on('enviardatosdemo', (data) => {
      console.log("Datos recibidos: ", data);
    })
  }
}
