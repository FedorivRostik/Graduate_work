import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Genre } from 'src/app/models/genres/genre.model';
import { ProductCreate } from 'src/app/models/products/productCreate.mode';
import { GenreService } from 'src/app/services/genre.service';
import { ProductService } from 'src/app/services/product.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-add-admin-product',
  templateUrl: './add-admin-product.component.html',
  styleUrls: ['./add-admin-product.component.css'],
})
export class AddAdminProductComponent implements OnInit {
  genres: Genre[] = [];
  createProduct!: ProductCreate;
  imageUrl?: string;

  productForm = new FormGroup({
    name: new FormControl('', Validators.required),
    slug: new FormControl('', Validators.required),
    amount: new FormControl('', Validators.required),
    price: new FormControl('', Validators.required),
    discount: new FormControl('', Validators.nullValidator),
    description: new FormControl('', Validators.nullValidator),
    genreId: new FormControl('', Validators.nullValidator),
  });

  constructor(
    private productService: ProductService,
    private genreService: GenreService,
    private _location: Location
  ) {
    this.createProduct = new ProductCreate();
  }

  ngOnInit(): void {
    this.genreService
      .getGenres()
      .subscribe((x) => (this.genres = <Genre[]>x.resultObj));
  }

  onSubmit() {
    console.log(this.productForm.valid);
    if (this.productForm.valid) {
      const productToCreate: ProductCreate = Object.assign(
        new ProductCreate(),
        this.productForm.value,
        { imageUrl: this.imageUrl }
      );
      this.productService
        .addProduct(productToCreate)
        .subscribe(() => this._location.back());
    }
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onloadend = () => {
      const baseString64 = reader.result as string;
      this.imageUrl = baseString64;
      console.log(baseString64);
    };

    if (file) {
      reader.readAsDataURL(file);
    }
  }
}
