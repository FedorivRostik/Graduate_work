import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { liqPayCheckoutForm } from 'src/app/models/LiqPay/liqPayCheckoutForm.model';
import { CartHeader } from 'src/app/models/cart/cartHeaders/cartHeader.model';
import { Cart } from 'src/app/models/cart/carts/cart.model';
import { HeaderUpdateShippmentInfoDto } from 'src/app/models/cart/carts/cartUpdateShippmentInfo.model';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-pay',
  templateUrl: './pay.component.html',
})
export class PayComponent implements OnInit {
  liqPayCheckoutForm!: liqPayCheckoutForm;
  isAddedInfo: boolean = false;
  isDataAvailable = false;
  cartHeader!: CartHeader;
  safeHtml!: SafeHtml;
  infoForm: FormGroup = new FormGroup({
    address: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    phone: new FormControl('', [Validators.required]),
  });

  constructor(
    private cartService: CartService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.cartService.getCheckoutModel().subscribe((x) => {
      if (x.isSuccess) {
        this.liqPayCheckoutForm = <liqPayCheckoutForm>x.resultObj;
        this.isDataAvailable = true;
      }
    });
    this.cartService.getCartHeader().subscribe((x) => {
      if (x.isSuccess) {
        this.cartHeader = <CartHeader>x.resultObj;

        if (
          this.cartHeader.email &&
          this.cartHeader.phone &&
          this.cartHeader.address
        ) {
          this.isAddedInfo = true;
          this.setPayBtn();
          this.infoForm.controls['address'].setValue(
            this.cartHeader?.address ?? ''
          );
          this.infoForm.controls['email'].setValue(
            this.cartHeader?.email ?? ''
          );
          this.infoForm.controls['phone'].setValue(
            this.cartHeader?.phone ?? ''
          );
        }
      }
    });
  }

  onSubmit() {
    if (this.infoForm.valid) {
      const productToCreate: HeaderUpdateShippmentInfoDto = Object.assign(
        new HeaderUpdateShippmentInfoDto(),
        this.infoForm.value,
        { cartHeaderId: localStorage.getItem('orderHeaderId_to_pay') }
      );

      this.cartService.updateShippmentInfo(productToCreate).subscribe((x) => {
        if (x.isSuccess) this.isAddedInfo = true;
        this.setPayBtn();
      });
    }
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

  getPayClasses() {
    if (!this.isAddedInfo) return 'max-w-0 max-h-0 overflow-hidden opacity-0';
    return 'max-w-none max-h-none overflow-hidden opacity-100';
  }

  private setPayBtn() {
    const htmlString = `<form
      method="POST"
      accept-charset="utf-8"
      action="https://www.liqpay.ua/api/3/checkout"
      *ngIf="isDataAvailable"
      >
      <input type="hidden" name="data" value="${this.liqPayCheckoutForm.data}" />
      <input
        type="hidden"
        name="signature"
        value="${this.liqPayCheckoutForm.signature}"
      />
      <div class="flex flex-col gap-1 duration-300" [class]="getPayClasses()">
        <span class="text-lg font-medium text-green-950 duration-300"
          >Visit Pay Page</span
        >
        <div>
          <button
            class="bg-green-400 font-medium px-7 py-2 flex gap-3 border-2 duration-300 border-green-400 text-white rounded-md hover:bg-green-100 hover:text-green-400 hover:border-green-400"
            [class.cursor-not-allowed]="!isAddedInfo"
            type="submit"
          >
            <img
              src="https://static.liqpay.ua/buttons/logo-small.png"
              name="btn_text"
            />
            <span>Pay</span>
          </button>
        </div>
      </div>
      </form>`;
    this.safeHtml = this.sanitizer.bypassSecurityTrustHtml(htmlString);
  }
}
