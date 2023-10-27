import { Component } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent {
  public userName = '';
  public password = '';

  constructor(private loginService: LoginService,
    private router: Router) {}

  login() {
    this.loginService.login(this.userName, this.password);
    this.router.navigate(['/ticket-list']);
  }
}
