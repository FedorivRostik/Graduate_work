import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CartAddDetails } from 'src/app/models/cart/cartDetails/cartAddDetails.model';
import { CartAddHeader } from 'src/app/models/cart/cartHeaders/cartAddHeader.model';
import { CartAdd } from 'src/app/models/cart/carts/cartAdd.model';
import { Product } from 'src/app/models/products/product.model';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-get-air-info',
  templateUrl: './get-air-info.component.html',
  styleUrls: ['./get-air-info.component.css'],
})
export class GetAirInfoComponent implements OnInit {
  radiationValue!: string;
  isReady: boolean = false;
  products: Product[] = [];
  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.radiationValue = this.route.snapshot.params['value'];
    this.isReady = true;
    this.productService
      .getProductsRadiation()
      .subscribe((x) => (this.products = <Product[]>x.resultObj));
  }

  onAddToCart(productId: string) {
    const cartHeader = new CartAddHeader();
    cartHeader.userId = this.authService.getUserId();

    const cartdetails = new CartAddDetails();
    cartdetails.productId = productId;
    cartdetails.count = 1;

    const cartAddModel = new CartAdd();
    cartAddModel.cartDetails = cartdetails;
    cartAddModel.cartHeader = cartHeader;
    this.cartService.upsertCart(cartAddModel).subscribe((x) => {
      if (x.isSuccess) {
        this.router.navigateByUrl('/cart');
      }
    });
  }
}
