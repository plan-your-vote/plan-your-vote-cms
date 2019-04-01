import { Component, OnInit } from '@angular/core';
import { PdfService } from './services/pdf.service';
import { Election } from './models/election';
import { Candidate } from './models/candidate';
import { ElectionService } from './services/election.service';
import { CandidateService } from './services/candidate.service';
import { ThemeService } from './services/theme.service';

const THEME_BASE_PATH = './assets/css';
const THEME_DEFAULT = '/default.css';
const THEME_MAPLE = '/maple.css';
const THEME_SNOWDROP = '/snowdrop.css';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  elections: Election[] = [];
  currentElection: Election;
  candidates: Candidate[];
  index: number;
  selectedCssFilepath: string;

  title = 'ClientApp';

  constructor(
    private themeService: ThemeService,
    private pdfService: PdfService,
    private electionApi: ElectionService,
    private candidatesApi: CandidateService
  ) {
    this.index = 0;

    this.electionApi.getElections().subscribe(res => {
      this.elections = res;
      this.currentElection = this.elections[this.index];
    });

    this.candidatesApi.getCandidates().subscribe(candidates => {
      this.candidates = candidates;
    });
  }

  ngOnInit(): void {
    this.themeService.getUserSelection().then(themeName => {
      this.chooseCss(themeName);
    });

    const logoImage = this.themeService.getImage('BCIT Logo');
  }

  chooseCss(option: string): void {
    switch (option) {
      case 'Maple':
        this.selectedCssFilepath = THEME_MAPLE;
        break;
      case 'Snowdrop':
        this.selectedCssFilepath = THEME_SNOWDROP;
        break;
      default:
        this.selectedCssFilepath = THEME_DEFAULT;
        break;
    }

    this.themeService.document
      .getElementById('theme')
      .setAttribute('href', `${THEME_BASE_PATH}${this.selectedCssFilepath}`);
  }

  public nextElection(): void {
    if (this.index != this.elections.length - 1) {
      this.index++;
    } else {
      this.index = 0;
    }
    this.currentElection = this.elections[this.index];
  }

  /**
   * Attached to 'Try PDF' button.
   * Currently passing all candidates.
   * Need to implement a way to implement just pass in candidate selected in the future.
   * Should rename pdf title title of current election.
   */
  generatePdf() {
    var pdfData: object = {
      dateTime: new Date().toLocaleString(),
      candidates: this.candidates
    };

    this.pdfService.pdf(pdfData, new Date().getHours().toString());
  }
}
