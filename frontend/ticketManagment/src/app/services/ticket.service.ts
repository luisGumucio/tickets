import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

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
}