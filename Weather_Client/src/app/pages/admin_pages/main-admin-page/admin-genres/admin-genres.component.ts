import { Component, OnInit } from '@angular/core';
import { Genre } from 'src/app/models/genres/genre.model';
import { GenreService } from 'src/app/services/genre.service';

@Component({
  selector: 'app-admin-genres',
  templateUrl: './admin-genres.component.html',
  styleUrls: ['./admin-genres.component.css'],
})
export class AdminGenresComponent implements OnInit {
  genres: Genre[] = [];
  constructor(private genreService: GenreService) {}
  ngOnInit(): void {
    this.genreService
      .getGenres()
      .subscribe((x) => (this.genres = <Genre[]>x.resultObj));
  }
}
