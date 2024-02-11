import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-payment-redirect',
  templateUrl: './payment-redirect.component.html',
  styleUrls: ['./payment-redirect.component.css'],
})
export class PaymentRedirectComponent implements OnInit {
  isResponded: boolean = false;
  isCorrect: boolean = false;

  constructor(private cartSerice: CartService, private router: Router) {}

  ngOnInit(): void {
    this.cartSerice.checkStatus().subscribe((x) => {
      if (x.isSuccess) {
        this.isResponded = true;
        this.isCorrect = <boolean>x.resultObj;
        setTimeout(() => this.router.navigateByUrl('/orders'), 1500);
      }
    });
  }
}
