import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/models/auth/loginRequest.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
})
export class LogInComponent {
  loginRequest: LoginRequest = new LoginRequest();

  constructor(private authService: AuthService, private router: Router) {}

  submit() {
    this.authService.login(this.loginRequest).subscribe(() => {
      this.router.navigateByUrl('/home');
    });
  }
}
