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
    return this.http.get<Candidate[]>('../assets/data/candidates.json');
  }
}
