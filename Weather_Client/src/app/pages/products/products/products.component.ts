import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/products/product.model';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe((data) => {
      console.log(this.products);
      this.products = data.resultObj ?? [];
    });
  }
}
