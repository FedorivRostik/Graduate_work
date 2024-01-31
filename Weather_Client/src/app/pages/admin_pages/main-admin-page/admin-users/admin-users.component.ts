import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user/user.model';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css'],
})
export class AdminUsersComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: UserService) {}
  ngOnInit(): void {
    this.userService
      .getUsers()
      .subscribe((x) => (this.users = <User[]>x.resultObj));
  }
}
