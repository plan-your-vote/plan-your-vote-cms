import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import en from 'src/assets/i18n/en.json';
import ko from 'src/assets/i18n/ko.json';
import zhcn from 'src/assets/i18n/zh-CN.json';
import zhhk from 'src/assets/i18n/zh-HK.json';
import zhtw from 'src/assets/i18n/zh-TW.json';

@Injectable({
  providedIn: 'root'
})
export class TranslateService {
  data: any = {};

  languages = ['en', 'ko', 'zh-CN', 'zh-HK', 'zh-TW'];

  constructor(private http: HttpClient) {}

  use(lang: string): Promise<{}> {
    return new Promise<{}>((resolve, reject) => {
      const langPath = '\/assets\/i18n\/' + (lang || 'en') + '.json';
      this.http.get<{}>(langPath).subscribe(
        translation => {
          this.data = Object.assign({}, translation || {});
          resolve(this.data);
        },
        error => {
          this.data = {};
          resolve(this.data);
        }
      );
    });
  }
}
