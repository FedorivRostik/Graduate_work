import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/response.model';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private products: Product[] = [];

  constructor(private http: HttpClient) {}

  getProducts(): Observable<ResponseModel<Product[]>> {
    return this.http
      .get<ResponseModel<Product[]>>('https://localhost:7000/api/products')
      .pipe();
  }
}
