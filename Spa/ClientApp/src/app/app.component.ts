import { Component, OnInit, NgModule } from '@angular/core';
import { PdfService } from './services/pdf.service';
import { Election } from './models/election';
import { ElectionService } from './services/election.service';
import { CandidateService } from './services/candidate.service';
import { ThemeService } from './services/theme.service';
import { FormsModule } from '@angular/forms';
import { Race } from './models/Race';
import { BallotIssue } from './models/BallotIssue';

const THEME_BASE_PATH = './assets/css';
const THEME_DEFAULT = '/default.css';
const THEME_MAPLE = '/maple.css';
const THEME_SNOWDROP = '/snowdrop.css';

@NgModule({
  imports: [
    FormsModule,
  ]
})
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  elections: Election[] = [];
  currentElection: Election;
  races: Race[];
  index: number;
  data: Election[] = [];
  issues: BallotIssue[] = [];
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
      this.data = res;
      this.currentElection = this.elections[this.index];
      
      //sets the current selection to the first one
      //TODO: do this async with a promise
      if (!this.currentElection) {
        this.currentElection = this.data[this.index];
        console.log(this.data);
      }
    });
    
    this.candidatesApi.getRaces().subscribe(races => {
      this.races = races;
    });
    
    this.candidatesApi.getBallotIssues().subscribe(ballotIssue => {
      this.issues = ballotIssue;
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

  /**
   * TODO: Remove, I don't think this function will be relevant once the application goes forward
   */
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
   */
  generatePdf() {
    console.log(this);
    //TODO: redo candidate selection id once dummy data added.
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
      races: this.races,
      ballotIssues: this.issues,
      selectedCandidateIds: selectedCandidateIds
    };

    this.pdfService.pdf(pdfData, this.currentElection.VoteTitle.replace(/[\W_]+/g," "));
  }
}
