import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Election } from 'src/app/models/election';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

const API_URL = "https://pyv.azurewebsites.net/api/election";

const httpOptions = {
  headers: new HttpHeaders(
  {
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ElectionService {
  elections = [];

  constructor(
    private http: HttpClient
  ) { }

  getElection() {
    return this.http.get<Election>(API_URL, httpOptions)
      .pipe(
        tap(),
        catchError(this.handleError('getElection', []))
      );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
