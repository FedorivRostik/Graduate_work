import { Injectable } from '@angular/core';
import { Product } from '../models/products/product.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/response.model';
import { ProductCreate } from '../models/products/productCreate.mode';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private products: Product[] = [];

  constructor(private http: HttpClient) {}

  getProducts(): Observable<ResponseModel<Product[]>> {
    return this.http
      .get<ResponseModel<Product[]>>('https://localhost:7000/api/products')
      .pipe();
  }

  getProductsRadiation(): Observable<ResponseModel<Product[]>> {
    return this.http
      .get<ResponseModel<Product[]>>(
        'https://localhost:7000/api/products/category/radiation'
      )
      .pipe();
  }

  addProduct(productCreate: ProductCreate) {
    return this.http
      .post<ResponseModel<Product>>(
        'https://localhost:7000/api/products',
        productCreate
      )
      .pipe();
  }
}
