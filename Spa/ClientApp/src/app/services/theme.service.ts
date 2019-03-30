import { Inject, Injectable } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { HttpClient } from '@angular/common/http';

const API_URL = "https://localhost:44307/api/themes/";

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  constructor(
    @Inject(DOCUMENT) public document: Document,
    private http: HttpClient
  ) {}

  private themeName: string;

  getUserSelection() {
    return new Promise<string>((resolve, reject) => {
      this.http.get<string>(API_URL).subscribe(
        res => {
          this.themeName = res;
          resolve(this.themeName);
        },
        error => {
          this.themeName = '';
          resolve(this.themeName);
        }
      );
    });
  }
}
