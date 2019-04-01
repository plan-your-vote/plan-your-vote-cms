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
  data: Election[] = [];
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
      this.data = res;
      
      //sets the current selection to the first one
      //TODO: do this async with a promise
      if (!this.currentElection) {
        this.currentElection = this.data[this.index];
        console.log(this.data);
      }
    });
    this.candidatesApi.getCandidates().subscribe(candidates => {
      this.candidates = candidates;
    });
  }

  ngOnInit(): void {
    this.themeService.getUserSelection().then(themeName => {
      this.chooseCss(themeName);
    });
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

  /**
   * TODO: I don't think this function will be relevant once the application goes forward
   */
  public nextElection(): void {
    this.currentElection = this.data[this.index];
    if (this.index != this.data.length - 1) {
      this.index++;
    } else {
      this.index = 0;
    }
  }

  /**
   * Attached to 'Try PDF' button.
   * Currently passing all candidates.
   * Needs to be divided by races instead of candidates.
   * Needs to implement candidate selection after Greg implements.
   */
  generatePdf() {
    let selectedCandidateIds = new Set();
    
    if (localStorage.getItem('candidates')) {
      let selectedCandidates = JSON.parse(localStorage.getItem('candidates'));

      selectedCandidates.forEach(c => {
        selectedCandidateIds.add(c.candidateId);
      });
    }

    var pdfData: object = {
      dateTime: new Date().toLocaleString(),
      electionInfo: this.currentElection,
      candidates: this.candidates,
      selectedCandidateIds: selectedCandidateIds
    };

    this.pdfService.pdf(pdfData, this.currentElection.VoteTitle);
  }
}
