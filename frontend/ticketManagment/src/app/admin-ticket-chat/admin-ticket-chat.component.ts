import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Router } from '@angular/router';
import { NewMessage } from '../models/message';
import { LoginService } from '../services/login.service';
import { TicketState } from '../models/ticket-state';

@Component({
  selector: 'app-admin-ticket-chat',
  templateUrl: './admin-ticket-chat.component.html',
  styleUrls: ['./admin-ticket-chat.component.scss'],
})
export class AdminTicketChatComponent implements OnInit {
  private connection: HubConnection;
  public conversation: NewMessage[] = [];
  ticketClient: any;
  public messageToSend = '';

  constructor(private router: Router, private loginService: LoginService) {
    this.connection = new HubConnectionBuilder()
      .withUrl('http://localhost:5039/ticketChatHub', {
        skipNegotiation: false,
        transport:
          signalR.HttpTransportType.WebSockets |
          signalR.HttpTransportType.LongPolling |
          signalR.HttpTransportType.ServerSentEvents,
      })
      .build();
    this.ticketClient = this.router.getCurrentNavigation()!.extras.state;
  }

  ngOnInit(): void {
    this.connection
      .start()
      .then((_) => {
        console.log('connected admin');
        this.join();
      })
      .catch((error) => {
        return console.error(error);
      });
    this.connection.on('NewMessage', (message) => this.newMessage(message));
  }

  private join() {
    this.connection
      .invoke('JoinGroup', this.ticketClient.email, this.loginService.getUserLogged())
      .then((_) => {
        const newMessage: NewMessage = {
          message: 'welcome',
          userName: this.loginService.getUserLogged(),
          groupName: this.ticketClient.email,
        };
        this.connection.invoke('SendMessage', newMessage).then((_) => {
          console.log('sending');
        });
      });
  }

  public sendMessage() {
    const newMessage: NewMessage = {
      message: this.messageToSend,
      userName: this.loginService.getUserLogged(),
      groupName: this.ticketClient.email,
    };
    this.connection
      .invoke('SendMessage', newMessage)
      .then((_) => (this.messageToSend = ''));
      
  }

  resolveTicket() {
    var ticket = this.ticketClient;
    ticket.ticketState = TicketState.DONE;
    this.connection
    .invoke('UdpateTicket', ticket);
  }

  private newMessage(message: NewMessage) {
    this.conversation.push(message);
  }
}
