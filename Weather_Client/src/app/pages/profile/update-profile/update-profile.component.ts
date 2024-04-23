import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HeaderUpdateShippmentInfoDto } from 'src/app/models/cart/carts/cartUpdateShippmentInfo.model';
import { User } from 'src/app/models/user/user.model';
import { UpdateUserPersonalParamsDto } from 'src/app/models/user/userUpdate.model';
import { AuthService } from 'src/app/services/auth.service';
import { PressureEnum } from 'src/app/utilities/enums/pressure.enum';
interface City {
  name: string;
  latitude: number;
  longitude: number;
}
@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.css'],
})
export class UpdateProfileComponent implements OnInit {
  infoForm!: FormGroup;
  cities: City[] = [
    { name: 'Київ', latitude: 50.45, longitude: 30.5233 },
    { name: 'Одеса', latitude: 46.4775, longitude: 30.7326 },
    { name: 'Дніпро', latitude: 48.4675, longitude: 35.04 },
    { name: 'Донецьк', latitude: 48.0028, longitude: 37.8053 },
    { name: 'Запоріжжя', latitude: 47.85, longitude: 35.1175 },
    { name: 'Львів', latitude: 49.8425, longitude: 24.0322 },
    { name: 'Кривий Ріг', latitude: 47.9086, longitude: 33.3433 },
    { name: 'Севастополь', latitude: 44.605, longitude: 33.5225 },
    { name: 'Миколаїв', latitude: 46.975, longitude: 31.995 },
    { name: 'Маріуполь', latitude: 47.0958, longitude: 37.5494 },
    { name: 'Вінниця', latitude: 49.2333, longitude: 28.4833 },
    { name: 'Макіївка', latitude: 48.0556, longitude: 37.9611 },
    { name: 'Сімферополь', latitude: 44.9484, longitude: 34.1 },
    { name: 'Полтава', latitude: 49.5894, longitude: 34.5514 },
    { name: 'Чернігів', latitude: 51.4939, longitude: 31.2947 },
    { name: 'Херсон', latitude: 46.6425, longitude: 32.625 },
    { name: 'Черкаси', latitude: 49.4444, longitude: 32.0597 },
    { name: 'Хмельницький', latitude: 49.4167, longitude: 27.0 },
    { name: 'Чернівці', latitude: 48.3, longitude: 25.9333 },
    { name: 'Суми', latitude: 50.9167, longitude: 34.75 },
  ];
  geoJsonFileNames = [
    'UA_05_Vinnytska.geojson',
    'UA_07_Volynska.geojson',
    'UA_09_Luhanska.geojson',
    'UA_12_Dnipropetrovska.geojson',
    'UA_14_Donetska.geojson',
    'UA_18_Zhytomyrska.geojson',
    'UA_21_Zakarpatska.geojson',
    'UA_23_Zaporizka.geojson',
    'UA_26_Ivano_Frankivska.geojson',
    'UA_32_Kyivska.geojson',
    'UA_35_Kirovohradska.geojson',
    'UA_43_Avtonomna_Respublika_Krym.geojson',
    'UA_46_Lvivska.geojson',
    'UA_48_Mykolaivska.geojson',
    'UA_51_Odeska.geojson',
    'UA_53_Poltavska.geojson',
    'UA_56_Rivnenska.geojson',
    'UA_59_Sumska.geojson',
    'UA_61_Ternopilska.geojson',
    'UA_63_Kharkivska.geojson',
    'UA_65_Khersonska.geojson',
    'UA_68_Khmelnytska.geojson',
    'UA_71_Cherkaska.geojson',
    'UA_74_Chernihivska.geojson',
    'UA_77_Chernivetska.geojson',
  ];
  districts: string[] = [];

  user?: User;
  pressureTypes: typeof PressureEnum = PressureEnum;
  constructor(
    private _authService: AuthService,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.geoJsonFileNames.forEach((element) => {
      this.readAndAddGeoJson(element);
    });
    this._authService.getProfile().subscribe((data) => {
      this.user = <User>data.resultObj;
      this.infoForm = new FormGroup({
        weight: new FormControl(this.user?.weight ?? '', Validators.required),
        age: new FormControl(this.user?.age ?? '', [Validators.required]),
        city: new FormControl(this.user?.city ?? '', [Validators.required]),
        region: new FormControl(this.user?.region ?? '', [Validators.required]),
        pressure: new FormControl(this.user?.pressure ?? 0, [
          Validators.required,
        ]),
        AvgUpSystolicPressure: new FormControl(
          this.user?.avgUpSystolicPressure ?? '',
          [Validators.required]
        ),
        AvgDownSystolicPressure: new FormControl(
          this.user?.avgDownSystolicPressure ?? 0,
          [Validators.required]
        ),
        AvgUpDialysticPressure: new FormControl(
          this.user?.avgDownSystolicPressure ?? '',
          [Validators.required]
        ),
        AvgDonwDialysticPressure: new FormControl(
          this.user?.avgDonwDialysticPressure ?? '',
          [Validators.required]
        ),
      });
    });
  }

  private readAndAddGeoJson(fileName: string): any {
    this.http
      .get(`./assets/geojsons/ukraine_geojson-master/${fileName}`)
      .subscribe((data: any) => {
        this.districts.push(data.properties['name:uk']);
      });
  }

  getErrorClasses(propKey: string): string {
    if (
      !(
        this.infoForm.controls[propKey].invalid &&
        this.infoForm.controls[propKey].touched
      )
    )
      return 'max-w-0 max-h-0 overflow-hidden opacity-0';
    return 'max-w-none max-h-none overflow-hidden opacity-100';
  }

  onSubmit() {
    console.log(this.infoForm);
    if (this.infoForm.valid) {
      const userUpdate: UpdateUserPersonalParamsDto = Object.assign(
        new UpdateUserPersonalParamsDto(),
        this.infoForm.value
      );
      console.log(userUpdate);
      this._authService.updateUser(userUpdate).subscribe((x) => {
        console.log(x);
        this.router.navigate(['/profile']);
      });
    }
  }
}
