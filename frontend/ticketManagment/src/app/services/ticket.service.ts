import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Ticket } from "../models/ticket";

@Injectable({
    providedIn: 'root'
})

export class TicketService {
    private baseUrl = 'http://localhost:5152/api/';
    private path = 'ticket';

    constructor(private http:HttpClient) {}

    getAllTickets(): Observable<any> {
        return this.http.get(`${this.baseUrl}${this.path}`);
    }
    saveTicket(ticket: Ticket): Observable<Ticket> {
        let body = JSON.stringify(ticket);
        let headers = new HttpHeaders({ 'Content-Type': 'application/JSON' });
        return this.http.post<Ticket>('http://localhost:5152/api/ticket', body, { headers: headers });
    }
}
