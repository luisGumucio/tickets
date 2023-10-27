import { Component, OnInit } from '@angular/core';
import { TicketService } from '../services/ticket.service';
import { Ticket } from '../models/ticket';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import * as signalR from '@microsoft/signalr';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.scss'],
})
export class TicketListComponent implements OnInit {
  private connection: HubConnection;
  tickets!: Ticket[];

  constructor(private ticketService: TicketService, private router: Router,
    private loginService: LoginService) {
    this.connection = new HubConnectionBuilder()
      .withUrl('http://localhost:5039/ticketHub', {
        skipNegotiation: false,
        transport:
          signalR.HttpTransportType.WebSockets |
          signalR.HttpTransportType.LongPolling |
          signalR.HttpTransportType.ServerSentEvents,
      })
      .build();
      this.connection
      .start()
      .then(() => console.log('Connection started.......!'))
      .catch(err => console.log('Error while connect with server'));
    this.connection.on('AddNewTicket', (message) => {
      this.newMessage(message);
    });
  }
  ngOnInit(): void {
    if (this.loginService.isAuthentication()) {
      this.getAllTickets();
    } else {
      this.router.navigate(['/login']);
      return;
    }
    
  }

  goToChat(ticket: Ticket) {
    this.router.navigate(['/admin-ticket-chat'], { state: ticket });
  }

  private getAllTickets() {
    this.ticketService.getAllTickets().subscribe((data) => {
      this.tickets = data;
    });
  }

  private newMessage(message: Ticket) {
    console.log(message);
    const ticketFilter = (b: any) => b.ticketId === message.ticketId;
    const index = this.tickets?.findIndex(ticketFilter);

if (index !== -1) {
    this.tickets[index] = message;
} else {
    this.tickets?.push(message);
}

    
  }
}
