import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Candidate } from 'src/app/models/candidate';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  candidates = [];

  constructor(private http: HttpClient) {}

  getCandidates() {
    return this.http.get<Candidate[]>('../assets/data/candidates.json');                     //DUMMY DATA
	  //return this.http.get<Candidate[]>('https://localhost:5001/api/candidates');               //DEV AND TESTING
    //return this.http.get<Candidate[]>('http://vote-web.azurewebsites.net/api/candidates');  //PRODUCTION
  }
}
