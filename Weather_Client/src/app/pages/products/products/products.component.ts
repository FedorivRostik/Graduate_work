import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartAddDetails } from 'src/app/models/cart/cartDetails/cartAddDetails.model';
import { CartAddHeader } from 'src/app/models/cart/cartHeaders/cartAddHeader.model';
import { CartAdd } from 'src/app/models/cart/carts/cartAdd.model';
import { Product } from 'src/app/models/products/product.model';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe((data) => {
      console.log(this.products);
      this.products = data.resultObj ?? [];
    });
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
