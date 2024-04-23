import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user/user.model';
import { AuthService } from 'src/app/services/auth.service';
import { PressureEnum } from 'src/app/utilities/enums/pressure.enum';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  user?: User;
  isloaded: boolean = false;
  constructor(private _authService: AuthService) {}
  ngOnInit(): void {
    this._authService.getProfile().subscribe((data) => {
      this.user = <User>data.resultObj;
      console.log(this.user);

      this.isloaded = true;
    });
  }
  pressureToString(pressure: PressureEnum): string {
    if (pressure === PressureEnum.Hypertensive) {
      return 'Гіпертонія';
    }
    if (pressure === PressureEnum.Hypotensive) {
      return 'Гіпотонія';
    }
    return 'Нема';
  }
}
