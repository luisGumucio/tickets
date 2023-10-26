import { Component, OnInit } from '@angular/core';
import { TicketService } from '../services/ticket.service';
import { Ticket } from '../models/ticket';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.scss'],
})
export class TicketListComponent implements OnInit {
  constructor(private ticketService: TicketService) {}

  tickets?: Ticket[];

  ngOnInit(): void {
    this.getAllTickets();
  }

  private getAllTickets() {
    this.ticketService.getAllTickets().subscribe((data) => {
      this.tickets = data;
    });
  }
}
