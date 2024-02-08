import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CartAdd } from '../models/cart/carts/cartAdd.model';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/response.model';
import { Cart } from '../models/cart/carts/cart.model';
import { CartUpdateDetails } from '../models/cart/cartDetails/cartUpdateDetails.model';

@Injectable({ providedIn: 'root' })
export class CartService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  upsertCart(cartAdd: CartAdd): Observable<ResponseModel<boolean>> {
    return this.http
      .post('https://localhost:7002/api/carts/cartUpsert', cartAdd)
      .pipe();
  }

  updateDetails(
    cartUpdateDetails: CartUpdateDetails
  ): Observable<ResponseModel<CartUpdateDetails>> {
    return this.http
      .post(
        'https://localhost:7002/api/carts/updateDetailsCount',
        cartUpdateDetails
      )
      .pipe();
  }

  getCart(): Observable<ResponseModel<Cart>> {
    return this.http
      .get(
        'https://localhost:7002/api/carts/getCart/' +
          this.authService.getUserId()
      )
      .pipe();
  }

  deleteDetails(
    carDetailId: CartUpdateDetails
  ): Observable<ResponseModel<boolean>> {
    return this.http
      .post('https://localhost:7002/api/carts/deleteDetails/', carDetailId)
      .pipe();
  }
}
