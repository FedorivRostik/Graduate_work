import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Genre } from '../models/genres/genre.model';
import { ResponseModel } from '../models/response.model';
import { Observable } from 'rxjs';
import { GenreCreate } from '../models/genres/genreCreate.model';

@Injectable({ providedIn: 'root' })
export class GenreService {
  constructor(private http: HttpClient) {}

  getGenres(): Observable<ResponseModel<Genre[]>> {
    return this.http
      .get<ResponseModel<Genre[]>>('https://localhost:7000/api/genres')
      .pipe();
  }

  addProduct(genreCreate: GenreCreate) {
    return this.http
      .post<ResponseModel<Genre>>(
        'https://localhost:7000/api/genres',
        genreCreate
      )
      .pipe();
  }
}
