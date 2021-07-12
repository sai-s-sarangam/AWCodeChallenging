//Sai: 07 / 11 / 2021: Movie Library Component for calling mvc controller service to get the data and bind them to html
import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subject } from 'rxjs';
import { Movie } from './Model/movie-library.model';

@Component({
  selector: 'app-movie-library',
  templateUrl: './movie-library.component.html'
})
export class MovieLibraryComponent implements OnDestroy, OnInit {

  dtOptions: DataTables.Settings = {};
  movies: Movie[] = [];
  private baseUrl = 'http://localhost:10855/';  //The base url will get at run time based on the architecture, it could be current web server or server based on the configuration file.
  private movieGetUrl = 'Default/GetData';  //Get from AWCodeChallenge MVC Controller. Added new Action Result in Default Controller.

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    withCredentials: true,
    observe: 'response' as 'response'
  };

  // We use this trigger because fetching the list of movies can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };
    this.httpClient.get<Movie[]>(`${this.baseUrl}${this.movieGetUrl}`, this.httpOptions)
      .subscribe(data => {
        this.movies = (data as any).body;
        // Calling the DT trigger to manually render the table
        this.dtTrigger.next();
      });
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
}
