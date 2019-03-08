import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Candidate } from 'src/app/models/candidate';

// const clientsUrl = "https://localhost:5001/api/candidates";              //DEV AND TESTING
const clientsUrl = "http://vote-web.azurewebsites.net/api/candidates"     //PRODUCTION

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  candidates = [];
  candidate;

  constructor(private http: HttpClient) {}

  getCandidates(): Observable<Candidate[]> {
    const url = `${clientsUrl}`
	  return this.http.get<Candidate[]>(url);
  }

  getCandidate(id: number): Observable<Candidate> {
    const url = `${clientsUrl}/${id}`;
    return this.http.get<Candidate>(url);
  }
}
