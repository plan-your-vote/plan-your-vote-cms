import { Component, OnInit, NgModule } from '@angular/core';
import { PdfService } from './services/pdf.service';
import { Election } from './models/election';
import { Candidate } from './models/candidate';
import { ElectionService } from './services/election.service';
import { CandidateService } from './services/candidate.service';
import { FormsModule } from '@angular/forms';

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
  data: Election[] = [];
  currentElection: Election;
  candidates: Candidate[];
  index: number;

  title = 'ClientApp';

  constructor(
    private pdfService: PdfService,
    private electionApi: ElectionService,
    private candidatesApi: CandidateService
  ) {
    this.index = 0;
    this.electionApi.getElections().subscribe(res => {
      this.data = res;
    });
    this.candidatesApi.getCandidates().subscribe(candidates => {
      this.candidates = candidates;
    });
  }

  ngOnInit(): void {
    this.nextElection();
  }

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
