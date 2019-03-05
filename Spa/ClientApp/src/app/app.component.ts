import { Component } from '@angular/core';
import { TranslateService } from './services/translate.service';
import { PdfService } from './services/pdf.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  constructor(
    private translate: TranslateService,
    private pdfService: PdfService
  ) {
    translate.use('en');
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
