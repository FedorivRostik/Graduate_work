import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from '../models/response.model';
import { NuclearResponse } from '../models/nuclearResponse.model';

@Injectable({ providedIn: 'root' })
export class NuclearService {
  constructor(private http: HttpClient) {}

  getByDistrict(district: string) {
    return this.http
      .get<ResponseModel<NuclearResponse>>(
        'https://localhost:7003/api/nuclears/disctrict/' + district
      )
      .pipe();
  }
}
