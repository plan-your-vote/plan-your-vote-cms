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
   *  Make this work for multiple races and ballots after aps is created.
   * 
   * @param pdfData PDF data passed through app.component.ts
   * @param fileName name of the file to be saved
   */
  public pdf(pdfData: object, fileName: string): void {
    //TODO: Remove this line when complete
    console.log(pdfData);
    
    let dummyCandidates = [{
      "firstName": "12345678901234567890",
      "lastName": "lastNameaaaaaaaa",
      "organization": "organization"
    },{
      "firstName": "2",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "3",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "4",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "5",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "6",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "7",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "8",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "firstName",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "9",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "10",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "11",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "12",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "13",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "14",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "15",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "16",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "17",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "18",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "19",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "20",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "21",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": ""
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": ""
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": ""
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": ""
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "22",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "23",
      "lastName": "lastName",
      "organization": "organization"
    },{
      "firstName": "24aaaaaaaaaaaaaaaaaaaaaa",
      "lastName": "lastName",
      "organization": "organization"
    }]

    //Setup
    this.doc = new jsPDF();

    //Default size is 210 x 297
    const MAX_PAGE_X = 210;
    const MAX_PAGE_Y = 297;

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
    let pageNumber = 1;

    let addDateTimeTitle = () => {
      const dateTimeTitleX = doubleSpace;
      const dateTimeTitleY = doubleSpace;
      this.doc.setFontSize(8);
      this.doc.text(pdfData["dateTime"], dateTimeTitleX, dateTimeTitleY);
      this.doc.text("Page " + pageNumber, MAX_PAGE_X - largeSpace, dateTimeTitleY);
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

      this.doc.addImage(base64image, 'JPEG', imageX, imageY, pdfImageWidth, pdfImageHeight);
    }

    let createFirstPage = (logo) => {
      addDateTimeTitle();
      
      addElectionLogo(logo);
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
      pageNumber++;
      this.doc.addPage();
      addDateTimeTitle();
      pageY += largeSpace;
    };

    let drawColumnDivider = (columnNumber) => {
      //Vertical lines between candidates
      //pageY should be set to baseHeight or baseHeightWithoutTitle
      this.doc.line(MAX_PAGE_X * (columnNumber - 1) / 4, pageY,
                    MAX_PAGE_X * (columnNumber - 1) / 4, MAX_PAGE_Y - doubleSpace);
    }

    let createCandidateCard = (candidate) => {
      //TODO: Add If selected, bold (and add a checkmark)?
      //Need to wait for selection feature
      //this.doc.setFontType("bold");

      this.doc.setFontSize(defaultFontSize);
      if (candidate.lastName.length + candidate.firstName.length > 20) {
        this.doc.text(candidate.lastName.toUpperCase() + ",", pageX, pageY);
        pageY += singleSpace;
        this.doc.text(candidate.firstName, pageX, pageY);
      } else {
        let candidateName = candidate.lastName.toUpperCase() + ", " + candidate.firstName;
        this.doc.text(candidateName, pageX, pageY);
      }

      if (candidate.organization) {
        pageY += singleSpace;
        this.doc.setFontSize(footerFontSize);
        this.doc.text(candidate.organization.toUpperCase(), pageX, pageY);
      }

      this.doc.setFontType("normal");
      pageY += doubleSpace + singleSpace;
    }

    let createRacePages = () => {
      //for loop this through each race once API is created.
      //I don't know what the object will look like so I don't want to mock it
      newPage();

      //TODO: add race title (ie mayer/councillor) when API is created
      //race title place holder ie (Mayer: 3 of 1)
      this.doc.setFontSize(headerFontSize);
      const raceTitle = "RACE TITLE: + #CANDIDATESSELECTED of #CANDIDATESNEEDED"
      this.doc.text(raceTitle, pageX, pageY);
      pageY += doubleSpace;
      
      //Horizontal line before candidates
      this.doc.line(pageX, pageY, MAX_PAGE_X - pageX, pageY);
      pageY += doubleSpace;

      //BaseHeight used to reset PageY after new column or new page
      const baseHeight = pageY;
      const baseHeightWithoutTitle = baseHeight - largeSpace;

      let columnNumber = 1;
      let candidatePageNumber = 1;

      const candidateList = dummyCandidates;
      //const candidateList = pdfData["candidates"]

      candidateList.forEach(candidate => {
        let pageLength = MAX_PAGE_Y - doubleSpace;
        let requiredHeight = singleSpace;
        
        if (candidate.lastName.length + candidate.firstName.length > 20) {
          requiredHeight += singleSpace;
        }
        if (candidate.organization) {
          requiredHeight += singleSpace;
        }
  
        if (requiredHeight + pageY > pageLength) {
          if (columnNumber === 4) {
            newPage();
            candidatePageNumber++;
            columnNumber = 1;
          } else {
            columnNumber++;
            pageX += MAX_PAGE_X / 4;
            pageY = candidatePageNumber === 1 ? baseHeight : baseHeightWithoutTitle;
            drawColumnDivider(columnNumber);
          }
        }
        createCandidateCard(candidate);
      });
    }
    
    let createBallotPages = () => {
      //need APIs to create this.

      //newPage();
    }

    let p1 = new Promise((resolve, reject) => {
      this.getBase64Image(pdfData["electionInfo"].LogoURL, resolve);
    });

    Promise.all([p1]).then((logo) => {
      createFirstPage(logo[0]);
      createRacePages();
      createBallotPages();

      this.doc.save(fileName);
    });
  }
}
