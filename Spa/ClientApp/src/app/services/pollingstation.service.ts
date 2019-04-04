import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PollingStation } from 'src/app/models/pollingstation';

// const API_URL = 'https://localhost:44307/api/PollingStations'; //DEV AND TESTING
const API_URL = "http://pyv.azurewebsites.net/api/PollingStations"     //PRODUCTION

@Injectable({
  providedIn: 'root'
})
export class PollingStationService {
  private pollingStations = [];

  constructor(private http: HttpClient) {}

  getPollingStations(): Promise<PollingStation[]> {
    return new Promise<PollingStation[]>((resolve, reject) => {
      this.http.get<{}>(API_URL).subscribe(
        data => {
          this.pollingStations = <PollingStation[]>data;
          resolve(this.pollingStations);
        },
        error => {
          this.pollingStations = [];
          resolve(this.pollingStations);
        }
      );
    });
  }
}
