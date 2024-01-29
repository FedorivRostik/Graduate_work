import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/response.model';
import { LoginResponse } from '../models/auth/loginResponse.model';
import { LoginRequest } from '../models/auth/loginRequest.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient) {}

  login(loginRequest: LoginRequest): Observable<ResponseModel<LoginResponse>> {
    return this.http
      .post<ResponseModel<LoginResponse>>(
        'https://localhost:7001/api/auth/login',
        loginRequest
      )
      .pipe();
  }

  checkIfAuth(): boolean {
    return !!!localStorage.getItem('access_token');
  }

  logout() {
    localStorage.removeItem('access_token');
  }
}
