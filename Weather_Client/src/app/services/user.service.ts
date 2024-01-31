import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/response.model';
import { User } from '../models/user/user.model';

@Injectable({ providedIn: 'root' })
export class UserService {
  private users: User[] = [];

  constructor(private http: HttpClient) {}

  getUsers(): Observable<ResponseModel<User[]>> {
    return this.http
      .get<ResponseModel<User[]>>('https://localhost:7001/api/auth/users')
      .pipe();
  }
}
