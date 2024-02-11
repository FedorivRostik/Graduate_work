import { Router } from '@angular/router';
import { CartStatusesEnum } from './../../utilities/enums/cartStatuses.enum';
import { Component, OnInit } from '@angular/core';
import { CartUpdateHeaderStatus } from 'src/app/models/cart/cartHeaders/cartUpdateHeaderStatus.model';
import { Cart } from 'src/app/models/cart/carts/cart.model';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
})
export class OrdersComponent implements OnInit {
  carts: Cart[] = [];
  openCarts: Cart[] = [];
  otherCarts: Cart[] = [];
  cartStatusesEnum: typeof CartStatusesEnum = CartStatusesEnum;
  constructor(private cartService: CartService, private router: Router) {}

  ngOnInit(): void {
    this.updateCarts();
  }

  onUpdateStatus(cart: Cart, cartStatusesEnum: CartStatusesEnum) {
    const cartheaderStatus: CartUpdateHeaderStatus = Object.assign(
      new CartUpdateHeaderStatus(),
      cart.cartHeader,
      { status: cartStatusesEnum }
    );
    this.cartService.updateHeaderStatus(cartheaderStatus).subscribe((x) => {
      if (x.isSuccess) {
        this.updateCarts();
      }
    });
  }

  onPay(cart: Cart) {
    this.cartService.setPay(cart);
  }

  getStatusClasses(status: string): string {
    switch (status) {
      case CartStatusesEnum.StatusOpen:
        return 'border-green-300  text-green-300 hover:text-white hover:bg-green-300 duration-300';
      case CartStatusesEnum.StatusPending:
        return 'border-blue-300  text-blue-300  hover:text-white hover:bg-blue-300 duration-300';
      case CartStatusesEnum.StatusCancelled:
        return 'border-red-300  text-red-300  hover:text-white hover:bg-red-300 duration-300';
      default:
        // Optional: Return an empty string for other statuses or throw an error
        return ''; // Or: throw new Error('Invalid status encountered');
    }
  }

  private updateCarts() {
    this.cartService.getUserCarts().subscribe((x) => {
      if (x.isSuccess) {
        this.carts = <Cart[]>x.resultObj;
        console.log(<Cart[]>x.resultObj);
        this.openCarts = this.carts.filter(
          (x) => x.cartHeader.status === CartStatusesEnum.StatusOpen
        );
        this.otherCarts = this.carts.filter(
          (x) => x.cartHeader.status !== CartStatusesEnum.StatusOpen
        );
      }
    });
  }
}
