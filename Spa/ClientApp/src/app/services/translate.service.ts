import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import en from 'src/assets/i18n/en.json';
import ko from 'src/assets/i18n/ko.json';
import zhcn from 'src/assets/i18n/zh-CN.json';
import zhtw from 'src/assets/i18n/zh-TW.json';

@Injectable({
  providedIn: 'root'
})
export class TranslateService {
  data: any = {};

  languages = [
    "en",
    "ko",
    "zh-CN",
    "zh-TW",
    "zh-HK",
  ];

  constructor(private http: HttpClient) { }

  use(lang: string): Promise<{}> {
    return new Promise<{}>((resolve, reject) => {
      switch (lang) {
        case 'ko':
          this.data = Object.assign({}, ko || {});
          return resolve(this.data);
        case 'zh-CN':
          this.data = Object.assign({}, zhcn || {});
          return resolve(this.data);
        case 'zh-TW':
        case 'zh-HK':
          this.data = Object.assign({}, zhtw || {});
          return resolve(this.data);
        case 'en':
        default:
          this.data = Object.assign({}, en || {});
          return resolve(this.data);
      }
    });
  }
}
