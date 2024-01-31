import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/products/product.model';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-admin-products',
  templateUrl: './admin-products.component.html',
  styleUrls: ['./admin-products.component.css'],
})
export class AdminProductsComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) {}
  ngOnInit(): void {
    this.productService
      .getProducts()
      .subscribe((x) => (this.products = <Product[]>x.resultObj));
  }
}
