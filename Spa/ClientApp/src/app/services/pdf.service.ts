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
   *  Make this work for multiple elections after dummy data is created.
   *  Converting Image to Base64 is an async call. would need to implement a promise before saving.
   * 
   * @param pdfData PDF data passed through app.component.ts
   * @param fileName name of the file to be saved
   */
  public pdf(pdfData: object, fileName: string): void {
    //TODO: Remove this line
    console.log(pdfData);

    //Setup
    this.doc = new jsPDF();
    //Default size is 210 x 297
    const MAX_PAGE_X = 210;
    const MAX_PAGE_y = 297;
    let pageX = 0;
    let pageY = 0;
    let addDateTimeTitle = () => {
      pageX += 10;
      pageY += 10;
      this.doc.setFontSize(8);
      this.doc.text(pdfData["dateTime"], pageX, pageY);
      this.doc.setFontSize(10);
      this.doc.text(pdfData["electionInfo"].VoteTitle, MAX_PAGE_X/2, pageY, {align:"center"});
    };
    let addBase64Image = (base64image) => {
      //console.log(base64image);
      this.doc.addImage(base64image, 'JPEG', 15, 40, 180, 160);
    }
    let createFirstPage = () => {
      addDateTimeTitle();
      //Adds election logo to the page
      //TODO: This is Async, so saving doesn't work at the moment.
      this.getBase64Image(pdfData["electionInfo"].LogoURL, addBase64Image);
    };
    let newElectionPage = () => {
      pageX = 0;
      pageY = 0;
      this.doc.addPage();
      addDateTimeTitle();
    };

    createFirstPage();
      
    pdfData["candidates"].forEach(candidate => {
      this.doc.setFontSize(12);
      let candidateName = candidate.firstName + " " + candidate.lastName;
      pageY += 10;
      this.doc.text(candidateName, pageX, pageY);
      pageY += 5;
      this.doc.setFontSize(10);
      this.doc.text(candidate.organization, pageX, pageY);
    });
    
    this.doc.save(fileName);
  }
}
