import { Component, OnInit, Inject, LOCALE_ID } from '@angular/core';
import { ShareService } from '@ngx-share/core';
import { TranslateService } from '../services/translate.service';

@Component({
  selector: 'app-footer-menu',
  templateUrl: './footer-menu.component.html',
  styleUrls: ['./footer-menu.component.less']
})
export class FooterMenuComponent implements OnInit {

  languages: string[] = [];
  defaultLanguage: string = window.navigator.language.includes("en-") ? "en" : window.navigator.language;

  constructor(public share: ShareService,
    private translateService: TranslateService,
    @Inject(LOCALE_ID) public locale: string) {
    this.languages = translateService.languages;
    // translateService.use(this.defaultLanguage);
  }

  ngOnInit() {
  }

  onLangChange(event) {
    this.translateService.use(event.target.value);
  }

}
