import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PollingStation } from 'src/app/models/pollingstation';

@Injectable({
  providedIn: 'root'
})
export class PollingStationService {
  pollingstations = [];

  constructor(private http: HttpClient) {}

  getPollingStations() {
    return this.http.get<PollingStation[]>('../assets/data/pollingstations.json');                     //DUMMY DATA
	  //return this.http.get<PollingStation[]>('https://localhost:5001/api/pollingstations');               //DEV AND TESTING
    //return this.https.get<PollingStation[]>('http://vote-web.azurewebsites.net/api/pollingstations');  //PRODUCTION
  }
}
