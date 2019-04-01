import { Inject, Injectable } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { HttpClient } from '@angular/common/http';

const API_URL = 'https://localhost:5001/api/theme/';

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
      this.http.get<any>(API_URL).subscribe(
        res => {
          this.themeName = res.selectedTheme.themeName;
          localStorage.setItem('images', JSON.stringify(res.images));
          resolve(this.themeName);
        },
        error => {
          this.themeName = '';
          resolve(this.themeName);
        }
      );
    });
  }

  // parameter `key` is case sensitive
  getImage(key: string) {
    const images = JSON.parse(localStorage.getItem('images'));

    for (let i = 0; i < images.length; i++) {
      if (images[i]['id'] === key) {
        return images[i];
      }
    }

    return { error: 'image not found: theme.service.ts' };
  }
}
