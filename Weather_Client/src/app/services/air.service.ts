import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from '../models/response.model';
import { airPollutionData } from '../models/air/airPollutionData.model';

@Injectable({ providedIn: 'root' })
export class AirService {
  constructor(private http: HttpClient) {}

  getByCity(latitude: number, longitude: number) {
    return this.http
      .get<ResponseModel<airPollutionData>>(
        'https://localhost:7004/api/air/city?latitude=' +
          latitude +
          '&longitude=' +
          longitude
      )
      .pipe();
  }
}
