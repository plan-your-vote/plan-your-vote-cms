import { Component, OnInit } from '@angular/core';
import { TranslateService } from './services/translate.service';
import { PdfService } from './services/pdf.service';
import { Election } from './models/election';
import { ElectionService } from './services/election.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  data: Election[] = [];
  currentElection: Election;
  index: number;

  title = 'ClientApp';

  constructor(
    private translate: TranslateService,
    private pdfService: PdfService,
    private electionApi: ElectionService
  ) {
    translate.use('en');

    this.index = 0;
    this.electionApi.getElections().subscribe(res => {
      this.data = res;
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

  setLang(lang: string) {
    this.translate.use(lang);
  }

  generatePdf() {
    var pdfData: object = {
      demoText: new Date().toLocaleString()
    };
    this.pdfService.pdf(pdfData, new Date().getHours().toString());
  }
}
