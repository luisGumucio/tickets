import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { NewMessage } from '../models/message';

@Component({
  selector: 'app-ticket-chat',
  templateUrl: './ticket-chat.component.html',
  styleUrls: ['./ticket-chat.component.scss'],
})
export class TicketChatComponent implements OnInit {
  public joined = false;
  private connection: HubConnection;
  public userName = '';
  public email = '';
  public messageToSend = '';
  public newChatOpen = false;
  public conversation: NewMessage[] = [];

  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl('http://localhost:5039/ticketChatHub', {
        skipNegotiation: false,
        transport:
          signalR.HttpTransportType.WebSockets |
          signalR.HttpTransportType.LongPolling |
          signalR.HttpTransportType.ServerSentEvents,
      })
      .build();
  }

  ngOnInit(): void {
    this.connection
      .start()
      .then((_) => {
        console.log('Connection Started');
      })
      .catch((error) => {
        return console.error(error);
      });
    this.connection.on('NewMessage', (message) => this.newMessage(message));
  }

  public join() {
    this.connection.invoke('JoinGroup', this.email, this.userName).then((_) => {
      this.joined = true;
    });
  }

  public sendMessage() {
    const newMessage: NewMessage = {
      message: this.messageToSend,
      userName: this.userName,
      groupName: this.email,
    };
    this.connection
      .invoke('SendMessage', newMessage)
      .then((_) => (this.messageToSend = ''));
  }

  private newMessage(message: NewMessage) {
    this.newChatOpen = true;
    this.conversation.push(message);
  }
}
