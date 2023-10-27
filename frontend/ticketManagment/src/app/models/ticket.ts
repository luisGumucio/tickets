import { TicketState } from "./ticket-state";

export class Ticket {
    ticketId!: number;
    title!: String;
    email!: String;
    createDate!: Date;
    ticketState!: TicketState
    department!: String
}