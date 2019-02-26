import { Component } from '@angular/core';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  title = 'app';

  //pdf generator
  download() {
    var data = document.getElementById('ConvertibleContent');
    html2canvas(data).then(canvas => {
      const contentDataURL = canvas.toDataURL('image/png')
      var imageWidth = 208;
      var imageHeight = canvas.height * imageWidth / canvas.width;
      let pdf = new jsPDF('p', 'mm', 'a4'); // A4 size page of PDF   
      pdf.addImage(contentDataURL, 'PNG', 0, 0, imageWidth, imageHeight)
      pdf.save('test.pdf'); // Generated PDF   
    });
  }
}
