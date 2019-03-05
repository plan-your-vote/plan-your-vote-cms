import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import en from 'src/assets/i18n/en.json';
import kr from 'src/assets/i18n/kr.json';
import zhcn from 'src/assets/i18n/zh-cn.json';
import zhtc from 'src/assets/i18n/zh-tc.json';

@Injectable({
  providedIn: 'root'
})
export class TranslateService {
  data: any = {};

  constructor(private http: HttpClient) {}

  use(lang: string): Promise<{}> {
    return new Promise<{}>((resolve, reject) => {
      switch (lang) {
        case 'kr':
          this.data = Object.assign({}, kr || {});
          return resolve(this.data);
        case 'zh-cn':
          this.data = Object.assign({}, zhcn || {});
          return resolve(this.data);
        case 'zh-tc':
          this.data = Object.assign({}, zhtc || {});
          return resolve(this.data);
        case 'en':
        default:
          this.data = Object.assign({}, en || {});
          return resolve(this.data);
      }
    });
  }
}
