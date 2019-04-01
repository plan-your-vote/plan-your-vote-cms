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
  website = 'https://vancouver.ca/plan-your-vote/index.aspx';
  description = "I voted"
  candidate: Candidate = {
    candidateId: null,
    firstName: '',
    lastName: '',
    picture: '',
    organization: '',
    selected: false
  };

  constructor(public share: ShareService,
              private translateService: TranslateService,
              private candidateService: CandidateService,
              @Inject(LOCALE_ID) public locale: string) {
    this.languages = translateService.languages;
  }

  getCandidate(id: number) {
    this.candidateService.getCandidate(id).subscribe(data => (this.candidate = data));
  }

  ngOnInit() {
    this.getCandidate(1);
  }

  onLangChange(event) {
    const arr = event.target.value.split(' ');
    const languageCode = arr.pop();

    this.translateService.use(languageCode);
  }

}