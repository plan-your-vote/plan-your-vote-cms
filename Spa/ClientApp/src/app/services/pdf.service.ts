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
    //TODO: Remove this line when complete
    console.log(pdfData);

    //Setup
    this.doc = new jsPDF();
    //Default size is 210 x 297
    const MAX_PAGE_X = 210;
    const MAX_PAGE_y = 297;
    //Change this value to change logo size
    const logoSize = 20;
    //Spacing
    const componentSpacing = 10;
    const componentPadding = 5;
    const titleFontSize = 20;
    const headerFontSize = 14;
    const defaultFontSize = 12;
    const footerFontSize = 10;

    let pageX = 0;
    let pageY = 0;
    let addDateTimeTitle = () => {
      pageX += componentSpacing;
      pageY += componentSpacing;
      this.doc.setFontSize(8);
      this.doc.text(pdfData["dateTime"], pageX, pageY);
      this.doc.setFontSize(10);
      this.doc.text(pdfData["electionInfo"].VoteTitle, MAX_PAGE_X/2, pageY, {align:"center"});
    };
    let addElectionLogo = (base64image) => {
      // avoid race conditions
      const imageX = componentSpacing;
      const imageY = 2 * componentSpacing;
      // auto scales image
      const imageData = this.doc.getImageProperties(base64image);
      const imageRatio = imageData.width / imageData.height;
      const pdfImageHeight = logoSize;
      const pdfImageWidth = pdfImageHeight * imageRatio;
      console.log(imageData);
      this.doc.addImage(base64image, 'JPEG', imageX, imageY, pdfImageWidth, pdfImageHeight);
      //TODO: Remove this line when complete
      //this.doc.save(fileName);
    }
    let createFirstPage = () => {
      addDateTimeTitle();
      pageY += componentSpacing;
      //Adds election logo to the page
      //TODO: This is Async, so saving doesn't work at the moment.
      this.getBase64Image(pdfData["electionInfo"].LogoURL, addElectionLogo);
      this.doc.setFontSize(titleFontSize);
      this.doc.text("My Summary", MAX_PAGE_X/2, pageY, {align:"center"});
      pageY += logoSize;
    };
    let newElectionPage = () => {
      pageX = 0;
      pageY = 0;
      this.doc.addPage();
      addDateTimeTitle();
    };

    createFirstPage();
      
    // pdfData["candidates"].forEach(candidate => {
    //   this.doc.setFontSize(defaultFontSize);
    //   let candidateName = candidate.firstName + " " + candidate.lastName;
    //   pageY += componentSpacing;
    //   this.doc.text(candidateName, pageX, pageY);
    //   pageY += componentPadding;
    //   this.doc.setFontSize(footerFontSize);
    //   this.doc.text(candidate.organization, pageX, pageY);
    // });
    
    //this.doc.save(fileName);
  }
}
