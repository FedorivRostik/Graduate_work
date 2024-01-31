import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { GenreCreate } from 'src/app/models/genres/genreCreate.model';
import { GenreService } from 'src/app/services/genre.service';

@Component({
  selector: 'app-add-admin-genre',
  templateUrl: './add-admin-genre.component.html',
  styleUrls: ['./add-admin-genre.component.css'],
})
export class AddAdminGenreComponent {
  genreForm = new FormGroup({
    name: new FormControl('', Validators.required),
  });

  constructor(private genreService: GenreService, private location: Location) {}

  onSubmit() {
    if (this.genreForm.valid) {
      const genreCreate: GenreCreate = Object.assign(
        new GenreCreate(),
        this.genreForm.value
      );
      this.genreService
        .addProduct(genreCreate)
        .subscribe(() => this.location.back());
    }
  }
}
