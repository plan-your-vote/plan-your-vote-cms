import { Injectable } from '@angular/core';
import * as jsPDF from 'jspdf';
import { DOCUMENT } from '@angular/platform-browser';
import { toBase64String } from '@angular/compiler/src/output/source_map';
import { Candidate } from '../models/candidate';

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
    const singleSpace = 5;
    const doubleSpace = 10;
    const largeSpace = 20;
    const titleFontSize = 20;
    const headerFontSize = 14;
    const defaultFontSize = 10;
    const footerFontSize = 8;

    let pageX = 0;
    let pageY = 0;

    let addDateTimeTitle = () => {
      const dateTimeTitleX = doubleSpace;
      const dateTimeTitleY = doubleSpace;
      this.doc.setFontSize(8);
      this.doc.text(pdfData["dateTime"], dateTimeTitleX, dateTimeTitleY);
      this.doc.setFontSize(10);
      this.doc.text(pdfData["electionInfo"].VoteTitle, MAX_PAGE_X/2, dateTimeTitleY, {align:"center"});
      pageX += singleSpace;
      pageY += doubleSpace;
    };

    let addElectionLogo = (base64image) => {
      // avoid race conditions for image placement
      const imageX = singleSpace;
      const imageY = singleSpace + doubleSpace;

      // auto scales image
      const imageData = this.doc.getImageProperties(base64image);
      const imageRatio = imageData.width / imageData.height;
      const pdfImageHeight = logoSize;
      const pdfImageWidth = pdfImageHeight * imageRatio;
      //TODO: Remove this line when complete
      console.log(imageData);
      this.doc.addImage(base64image, 'JPEG', imageX, imageY, pdfImageWidth, pdfImageHeight);
      //TODO: Remove this line when complete
      //this.doc.save(fileName);
    }

    let createFirstPage = () => {
      addDateTimeTitle();
      //Adds election logo to the page
      //TODO: This is Async, so saving doesn't work at the moment.
      this.getBase64Image(pdfData["electionInfo"].LogoURL, addElectionLogo);
      pageY += largeSpace;

      this.doc.setFontSize(titleFontSize);
      this.doc.text("My Summary", MAX_PAGE_X/2, pageY, {align:"center"});
      pageY += logoSize;

      this.doc.setFontSize(headerFontSize);
      this.doc.text("My Voting Day", pageX, pageY);
      pageY += singleSpace;

      this.doc.text("Information to be grabbed when API is created", pageX, pageY);
      pageY += singleSpace;

      this.doc.line(pageX, pageY, MAX_PAGE_X - pageX, pageY);
      pageY += singleSpace;
      
      this.doc.text("What to bring", pageX, pageY);
      pageY += singleSpace;

      this.doc.text("Information to be grabbed when API is created", pageX, pageY);
      pageY += singleSpace;
    };

    let newPage = () => {
      pageX = 0;
      pageY = 0;
      this.doc.addPage();
      addDateTimeTitle();
      pageY += largeSpace;
    };

    let drawLineDividers = () => {
      //Horizontal line before candidates
      this.doc.line(pageX, pageY, MAX_PAGE_X - pageX, pageY);
      pageY += doubleSpace;

      //Vertical lines between candidates
      this.doc.line(MAX_PAGE_X / 4, pageY - singleSpace, MAX_PAGE_X / 4, MAX_PAGE_y - largeSpace);
      this.doc.line(MAX_PAGE_X / 2, pageY - singleSpace, MAX_PAGE_X / 2, MAX_PAGE_y - largeSpace);
      this.doc.line(MAX_PAGE_X * 3 / 4, pageY - singleSpace, MAX_PAGE_X * 3 / 4, MAX_PAGE_y - largeSpace);
    }

    let createCandidateCard = (candidate) => {
      this.doc.setFontSize(defaultFontSize);
      let candidateName = candidate.lastName.toUpperCase() + ", " + candidate.firstName;
      this.doc.text(candidateName, pageX, pageY);
      pageY += doubleSpace;
      this.doc.setFontSize(footerFontSize);

      if(candidate.organization) {
        this.doc.text(candidate.organization, pageX, pageY);
        pageY += singleSpace;
      }
      //TODO: Add If selected, add a checkmark. Need to wait for selection feature
    }

    let createRacePages = () => {
      //for loop this through each race once API is created.
      //I don't know what the object will look like so I don't want to mock it

      newPage();

      //TODO: add race title (ie mayer/councillor) when API is created
      //race title place holder ie (Mayer: 3 of 1)
      this.doc.setFontSize(headerFontSize);
      let raceTitle = "RACE TITLE: + #CANDIDATESSELECTED of #CANDIDATESNEEDED"
      this.doc.text(raceTitle, pageX, pageY);
      pageY += doubleSpace;

      drawLineDividers();
      pdfData["candidates"].forEach(candidate => {
        createCandidateCard(candidate);
      });
    }
    
    let createBallotPages = () => {
      //need APIs to create this.

      //newPage();
    }

    createFirstPage();
    createRacePages();
    createBallotPages();
    
    this.doc.save(fileName);
  }
}
