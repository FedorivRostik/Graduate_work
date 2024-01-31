import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterRequest } from 'src/app/models/auth/registerRequest.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent {
  registerRequest: RegisterRequest;

  constructor(private authService: AuthService, private router: Router) {
    this.registerRequest = new RegisterRequest();
  }
  onRegister() {
    this.authService
      .register(this.registerRequest)
      .subscribe(() => this.router.navigateByUrl('/log-in'));
  }
}
