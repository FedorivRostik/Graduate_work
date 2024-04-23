import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { AppRoles } from 'src/app/utilities/enums/appRoles.enums';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isLogged: boolean = false;
  userRole!: AppRoles;
  userName!: string;
  public appRoles: typeof AppRoles;

  constructor(private authService: AuthService) {
    this.appRoles = AppRoles;
  }

  ngOnInit(): void {
    this.isLogged = this.authService.checkIfAuth();
    this.userRole = this.authService.getUserRole();
    this.userName = this.authService.getUserName();
  }

  onLogOut(): void {
    this.authService.logout();
  }

  onRole(appRole: AppRoles): boolean {
    return this.authService.IsRole(appRole);
  }
}
