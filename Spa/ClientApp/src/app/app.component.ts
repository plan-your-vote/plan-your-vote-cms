import { Component } from '@angular/core';
import { TranslateService } from './services/translate.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  constructor(private translate: TranslateService) {
    translate.use('en').then(() => {
      console.log(translate.data);
    });
  }

  setLang(lang: string) {
    this.translate.use(lang);
  }
}
