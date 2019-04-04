import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Candidate } from 'src/app/models/candidate';
import { Race } from '../models/Race';
import { BallotIssue } from '../models/BallotIssue';

// const clientsUrl = "https://localhost:5001/api/races";              // DEV AND TESTING
const clientsUrl = "http://pyv.azurewebsites.net/api"     // PRODUCTION

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  races = [];
  candidates = [];

  constructor(private http: HttpClient) {}

  getRaces() {
    const url = `${clientsUrl}/races`;
    return this.http.get<Race[]>(url);
  }

  getCandidates() {
    const url = `${clientsUrl}/candidates`;
	  return this.http.get<Candidate[]>(url);
  }

  getCandidate(id: number) {
    const url = `${clientsUrl}/candidates/${id}`;
    return this.http.get<Candidate>(url);
  }

  getBallotIssues() {
    const url = `${clientsUrl}/ballotissues`;
    return this.http.get<BallotIssue[]>(url);

  }
  getSingle<T>(id: number) {
    return this.http.get<T>(`${clientsUrl}/candidates/${id}`);
  }
}
