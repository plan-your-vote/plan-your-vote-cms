import { Component, OnInit, Inject, LOCALE_ID } from '@angular/core';
import { ShareService } from '@ngx-share/core';
import { TranslateService } from '../services/translate.service';
import { CandidateService } from '../services/candidate.service';
import { Candidate } from 'src/app/models/candidate';

@Component({
  selector: 'app-footer-menu',
  templateUrl: './footer-menu.component.html',
  styleUrls: ['./footer-menu.component.less']
})
export class FooterMenuComponent implements OnInit {

  languages: string[] = [];
  defaultLanguage: string = window.navigator.language.includes("en-") ? "en" : window.navigator.language;
  candidates: Candidate[] = [];
  description: string;

  constructor(public share: ShareService,
              private translateService: TranslateService,
              private candidateService: CandidateService,
              @Inject(LOCALE_ID) public locale: string) {
    this.languages = translateService.languages;
    // translateService.use(this.defaultLanguage);
  }

  getCandidates(): void {
    this.candidateService.getCandidates().subscribe(data => (this.candidates = data));
  }

  ngOnInit() {
    this.getCandidates();
    this.description = this.candidates[0].firstName;
  }

  onLangChange(event) {
    this.translateService.use(event.target.value);
  }

}
