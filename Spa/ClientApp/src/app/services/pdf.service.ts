import { Injectable } from '@angular/core';
import * as jsPDF from 'jspdf';
import { DOCUMENT } from '@angular/platform-browser';
import { toBase64String } from '@angular/compiler/src/output/source_map';

@Injectable({
  providedIn: 'root'
})
export class PdfService {

  doc: jsPDF = new jsPDF();
  constructor() {
  }

  /**
   * Converts image to base64 string.
   * Need proxyURL to bypass CORS
   * 
   * @param targetUrl URL of image
   * @param callback callback
   */
  public getBase64Image(targetUrl, callback) {
    var xhr = new XMLHttpRequest();
    xhr.onload = function () {
        var reader = new FileReader();
        reader.onloadend = function () {
            callback(reader.result);
        };
        reader.readAsDataURL(xhr.response);
    };
    var proxyUrl = 'https://cors-anywhere.herokuapp.com/';
    xhr.open('GET', proxyUrl + targetUrl);
    xhr.responseType = 'blob';
    xhr.send();
  };
  
  /**
   * Creates pdf.
   * 
   * TODO:
   *  Only send candidates user selects?
   *  Should there be one page per candidate?
   *  Converting Image to Base64 is an async call. would need to implement a promise before saving.
   *  Currently saving after image is converted
   * 
   * @param pdfData PDF data passed through app.component.ts
   * @param fileName name of the file to be saved
   */
  public pdf(pdfData: object, fileName: string): void {
    this.doc = new jsPDF();

    this.doc.text(pdfData["dateTime"], 10, 10);
      
    // pdfData["candidates"].forEach(candidate => {
    //   //this.doc.addPage();
    //   this.doc.text(candidate.firstName, 10, 20);
    // });

    //Hard coded for demo purposes
    this.doc.text(pdfData["candidates"][0].firstName, 10, 20);
    this.doc.text(pdfData["candidates"][0].lastName, 10, 30);
    this.getBase64Image('https://vancouver.ca/plan-your-vote/img/mayor1.jpg', function(base64image) {
      console.log(base64image);
      this.doc.addImage(base64image, 'JPEG', 15, 40, 180, 160);
      this.doc.save(fileName);
    }.bind(this));
  }
}
