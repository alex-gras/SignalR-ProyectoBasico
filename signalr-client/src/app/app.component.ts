import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { SignalrService } from './services/signalr.service';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-root',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'signalr-client';

  user = 'ANGULAR';
  message = 'demo send';

  constructor(public signalRService: SignalrService
    , private http: HttpClient) {}

    ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addMessageListener();
    this.signalRService.addSendMessageListener();
    // this.startHttpRequest();
  }

  private startHttpRequest = () => {
    this.http.get('https://localhost:6001/api/notification')
      .subscribe(res => {
        console.log("startHttpRequest:");
        console.log(res);
      })
  }

  sendMessage(): void {
    if (this.user && this.message) {
      const data  = [this.user, this.message];
      this.signalRService.sendMessage(data);
      this.message = '';
    }
  }





}
