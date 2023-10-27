import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})

export class LoginService {
    private user?: string;
    private password?: string;

    public isAuthentication(): boolean {
        return this.user !==  undefined && this.password !== undefined;
    }

    public login(user: string, password: string) {
        this.user = user;
        this.password = password;
    }

    public getUserLogged(): string {
        return this.user!;
    }
}