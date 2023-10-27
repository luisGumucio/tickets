import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TicketService } from '../services/ticket.service';
import { TicketState } from '../models/ticket-state';
import { Ticket } from '../models/ticket';

@Component({
  selector: 'app-ticket-create',
  templateUrl: './ticket-create.component.html',
  styleUrls: ['./ticket-create.component.scss'],
})
export class TicketCreateComponent implements OnInit {
  ticket = this.formBuilder.group({
    title: ['', Validators.required],
    emailAddres: ['', [Validators.required, Validators.email]],
    department: ['', Validators.required],
  });
  submitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private ticketService: TicketService
  ) {}
  ngOnInit() {}
  get f() {
    return this.ticket.controls;
  }

  onSubmit() {
    this.submitted = true;
    if (this.ticket.invalid) {
      return;
    }
    var data = new Ticket();
    data.title = this.ticket.value.title!;
    data.email = this.ticket.value.emailAddres!;
    data.department = this.ticket.value.department!;
    data.ticketState = TicketState.OPEN;
    this.ticketService.saveTicket(data).subscribe(
      () => {
        this.ticket.setValue({title: '', emailAddres: '', department: ''});
        alert("Ticket created successfully!");
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
