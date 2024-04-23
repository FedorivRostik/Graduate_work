import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ResponseModel } from '../models/response.model';
import { LoginResponse } from '../models/auth/loginResponse.model';
import { LoginRequest } from '../models/auth/loginRequest.model';
import { jwtDecode } from 'jwt-decode';
import { AppRoles } from '../utilities/enums/appRoles.enums';
import { RegisterRequest } from '../models/auth/registerRequest.model';
import { User } from '../models/user/user.model';
import { UpdateUserPersonalParamsDto } from '../models/user/userUpdate.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient) {}

  login(loginRequest: LoginRequest): Observable<ResponseModel<LoginResponse>> {
    return this.http
      .post<ResponseModel<LoginResponse>>(
        'https://localhost:7001/api/auth/login',
        loginRequest
      )
      .pipe(
        tap((response: any) => {
          localStorage.setItem('access_token', response.resultObj?.token!);
          localStorage.setItem('user_role', this.getUserRoleForLocalStorage());
          localStorage.setItem('user_id', this.getUserIdForLocalStorage());
          localStorage.setItem('user_name', this.getUserNameForLocalStorage());
        })
      );
  }

  register(
    registerRequest: RegisterRequest
  ): Observable<ResponseModel<boolean>> {
    return this.http
      .post<ResponseModel<boolean>>(
        'https://localhost:7001/api/auth/register',
        registerRequest
      )
      .pipe();
  }

  updateUser(updateUserPersonalParamsDto: UpdateUserPersonalParamsDto) {
    return this.http.put(
      'https://localhost:7001/api/auth/profile',
      updateUserPersonalParamsDto
    );
  }

  getProfile(): Observable<ResponseModel<User>> {
    return this.http.get<ResponseModel<User>>(
      'https://localhost:7001/api/auth/profile'
    );
  }

  checkIfAuth(): boolean {
    return !!!localStorage.getItem('access_token');
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('user_role');
    localStorage.removeItem('user_id');
    localStorage.removeItem('user_name');
  }

  getUserRole(): AppRoles {
    return <AppRoles>localStorage.getItem('user_role') ?? AppRoles.Guest;
  }

  getUserId(): string {
    return <AppRoles>localStorage.getItem('user_id');
  }
  getUserName(): string {
    return localStorage.getItem('user_name') ?? '';
  }

  IsRole(appRole: AppRoles): boolean {
    return this.getUserRole() === appRole;
  }

  private getUserIdForLocalStorage() {
    const tokenStr = localStorage.getItem('access_token');
    if (tokenStr != null) {
      try {
        const bearerToken: any = jwtDecode(tokenStr);
        console.warn(bearerToken);
        return bearerToken.sub;
      } catch (err) {
        return null;
      }
    }
  }
  private getUserNameForLocalStorage() {
    const tokenStr = localStorage.getItem('access_token');
    if (tokenStr != null) {
      try {
        const bearerToken: any = jwtDecode(tokenStr);
        console.warn(bearerToken);
        return bearerToken.name;
      } catch (err) {
        return null;
      }
    }
  }
  private getUserRoleForLocalStorage(): AppRoles {
    const tokenStr = localStorage.getItem('access_token');
    if (tokenStr != null) {
      try {
        const bearerToken: any = jwtDecode(tokenStr);
        return bearerToken.role;
      } catch (err) {
        return AppRoles.Guest;
      }
    }
    return AppRoles.Guest;
  }
}
