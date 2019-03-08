import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Candidate } from 'src/app/models/candidate';

// const clientsUrl = "https://localhost:5001/api/candidates";              //DEV AND TESTING
const clientsUrl = "http://cityvote.azurewebsites.net/api/candidates"     //PRODUCTION

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  candidates = [];

  constructor(private http: HttpClient) {}

  getCandidates() {
    const url = `${clientsUrl}`
	  return this.http.get<Candidate[]>(url);
  }

  getCandidate(id: number) {
    const url = `${clientsUrl}/${id}`;
    return this.http.get<Candidate>(url);
  }
}
