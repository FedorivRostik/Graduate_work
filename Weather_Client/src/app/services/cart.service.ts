import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CartAdd } from '../models/cart/carts/cartAdd.model';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/response.model';
import { Cart } from '../models/cart/carts/cart.model';
import { CartUpdateDetails } from '../models/cart/cartDetails/cartUpdateDetails.model';
import { CartUpdateHeaderStatus } from '../models/cart/cartHeaders/cartUpdateHeaderStatus.model';
import { Router } from '@angular/router';
import { liqPayCheckoutForm } from '../models/LiqPay/liqPayCheckoutForm.model';
import { LiqPayCreate as LiqPayCreateDto } from '../models/LiqPay/liqPayCreate.model';
import { HeaderUpdateShippmentInfoDto } from '../models/cart/carts/cartUpdateShippmentInfo.model';
import { CartHeader } from '../models/cart/cartHeaders/cartHeader.model';
import { LiqPayCheckStatus } from '../models/LiqPay/liqPayCheckStatus.model';

@Injectable({ providedIn: 'root' })
export class CartService {
  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private router: Router
  ) {}

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

  updateHeaderStatus(
    cartUpdateHeaderStatus: CartUpdateHeaderStatus
  ): Observable<ResponseModel<boolean>> {
    return this.http
      .post(
        'https://localhost:7002/api/carts/updateStatus',
        cartUpdateHeaderStatus
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

  getUserCarts(): Observable<ResponseModel<Cart[]>> {
    return this.http.get('https://localhost:7002/api/carts/userOrders').pipe();
  }

  deleteDetails(
    carDetailId: CartUpdateDetails
  ): Observable<ResponseModel<boolean>> {
    return this.http
      .post('https://localhost:7002/api/carts/deleteDetails/', carDetailId)
      .pipe();
  }

  setPay(cart: Cart): void {
    localStorage.setItem('orderHeaderId_to_pay', cart.cartHeader.cartHeaderId);
    localStorage.setItem(
      'orderAmount_to_pay',
      cart.cartHeader.cartTotal?.toString() ?? 'error'
    );
    if (localStorage.getItem('orderAmount_to_pay') !== 'error') {
      this.router.navigateByUrl('/pay');
    }
  }

  updateShippmentInfo(
    cartUpdateShippmentInfo: HeaderUpdateShippmentInfoDto
  ): Observable<ResponseModel<boolean>> {
    return this.http.post(
      'https://localhost:7002/api/carts/cartUpdateShippmentInfo',
      cartUpdateShippmentInfo
    );
  }

  getCheckoutModel(): Observable<ResponseModel<liqPayCheckoutForm>> {
    const LiqPayCreateDto: LiqPayCreateDto = this.getCreateLiqPay();

    return this.http.post(
      'https://localhost:7002/api/carts/liqpayData',
      LiqPayCreateDto
    );
  }

  getCartHeader(): Observable<ResponseModel<CartHeader>> {
    return this.http.get(
      'https://localhost:7002/api/carts/GetCartHeader/' +
        localStorage.getItem('orderHeaderId_to_pay')!
    );
  }

  checkStatus(): Observable<ResponseModel<boolean>> {
    const cartHeaderId = localStorage.getItem('orderHeaderId_to_pay')!;
    const dto: LiqPayCheckStatus = new LiqPayCheckStatus();
    dto.cartHeaderId = cartHeaderId;
    return this.http.post('https://localhost:7002/api/carts/checkStatus', dto);
  }

  private getCreateLiqPay(): LiqPayCreateDto {
    const liqPayCreate: LiqPayCreateDto = new LiqPayCreateDto();
    liqPayCreate.orderId = localStorage.getItem('orderHeaderId_to_pay')!;
    liqPayCreate.amount = +localStorage.getItem('orderAmount_to_pay')!;
    return liqPayCreate;
  }
}
