import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartDetails } from 'src/app/models/cart/cartDetails/cartDetails.model';
import { CartUpdateDetails } from 'src/app/models/cart/cartDetails/cartUpdateDetails.model';
import { CartUpdateHeaderStatus } from 'src/app/models/cart/cartHeaders/cartUpdateHeaderStatus.model';
import { Cart } from 'src/app/models/cart/carts/cart.model';
import { CartService } from 'src/app/services/cart.service';
import { CartStatusesEnum } from 'src/app/utilities/enums/cartStatuses.enum';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
})
export class CartComponent implements OnInit {
  cart!: Cart;

  constructor(private cartService: CartService, private router: Router) {}

  ngOnInit(): void {
    this.cartService
      .getCart()
      .subscribe((x) => (this.cart = <Cart>x.resultObj));
  }

  onIncrementDetails(count: number): number {
    return ++count;
  }

  onDecrementDetails(count: number): number {
    return --count;
  }

  onUpdateDetails(
    cartDetails: CartDetails,
    countChange: (count: number) => number
  ) {
    const cartUpdateDetails: CartUpdateDetails = Object.assign(
      new CartUpdateDetails(),
      cartDetails
    );

    cartUpdateDetails.count = countChange(cartUpdateDetails.count);
    this.cartService.updateDetails(cartUpdateDetails).subscribe((x) => {
      if (x.isSuccess) {
        const updatedDetails = <CartUpdateDetails>x.resultObj;
        const updatedIndex = this.cart.cartDetails.findIndex(
          (x) => x.cartDetailsId === updatedDetails.cartDetailsId
        );
        this.cart.cartDetails[updatedIndex].count = updatedDetails.count;
        this.updateCartTotal();
      }
    });
  }

  onDeleteDetails(cartDetailsId: string) {
    const cartUpdateDetails: CartUpdateDetails = new CartUpdateDetails();
    cartUpdateDetails.cartDetailsId = cartDetailsId;
    this.cartService.deleteDetails(cartUpdateDetails).subscribe((x) => {
      if (x.isSuccess) {
        const deletedIndex = this.cart.cartDetails.findIndex(
          (d) => d.cartDetailsId == cartDetailsId
        );
        if (deletedIndex > -1) {
          this.cart.cartDetails.splice(deletedIndex, 1);
          this.updateCartTotal();
        }
      }
    });
  }

  onCheckout() {
    const cartheaderStatus: CartUpdateHeaderStatus = Object.assign(
      new CartUpdateHeaderStatus(),
      this.cart.cartHeader,
      { status: CartStatusesEnum.StatusPending }
    );
    this.cartService.updateHeaderStatus(cartheaderStatus).subscribe((x) => {
      if (x.isSuccess) {
        this.router.navigateByUrl('/orders');
      }
    });
  }

  private updateCartTotal() {
    console.log(this.cart.cartDetails);
    this.cart.cartHeader.cartTotal = +this.cart.cartDetails
      .reduce((partialSum, a) => partialSum + a.price * a.count, 0)
      .toFixed(2);
    console.log(this.cart.cartHeader.cartTotal);
    this.cart.cartHeader.discount = +this.cart.cartDetails
      .reduce((partialSum, a) => partialSum + <number>a.discount * a.count, 0)
      .toFixed(2);
  }
}
