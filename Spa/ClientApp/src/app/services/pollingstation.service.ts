import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PollingStation } from 'src/app/models/pollingstation';

const clientsUrl = "https://localhost:44307/api/PollingStations";              //DEV AND TESTING
//const clientsUrl = "http://cityvote.azurewebsites.net/api/PollingStations"     //PRODUCTION

@Injectable({
  providedIn: 'root'
})
export class PollingStationService {
  pollingstations = [];

  constructor(private http: HttpClient) { }

  getPollingStations() {
    const url = `${clientsUrl}`
    return this.http.get<PollingStation[]>(url);
  }
}
