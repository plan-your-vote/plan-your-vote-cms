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
    
    let dummyRaces = [{
      "PositionName": "Mayor",
      "NumberNeeded": 1,
      "candidates": [{
        "firstName": "Mayor1",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 1
      },{
        "firstName": "Mayor2",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 2
      },{
        "firstName": "Mayor3",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 3
      },{
        "firstName": "Mayor4",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 4
      },{
        "firstName": "Mayor5",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 5
      },{
        "firstName": "Mayor6",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 6
      },{
        "firstName": "Mayor4",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 4
      },{
        "firstName": "Mayor7",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 7
      },{
        "firstName": "Mayor8",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 8
      },{
        "firstName": "Mayor9",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 9
      },{
        "firstName": "Mayor10",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 10
      },{
        "firstName": "Mayor11",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 11
      },{
        "firstName": "Mayor12",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 12
      },{
        "firstName": "Mayor13",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 13
      },{
        "firstName": "Mayor14",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 14
      },{
        "firstName": "Mayor15",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 15
      },{
        "firstName": "Mayor16",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 16
      },{
        "firstName": "Mayor17",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 17
      },{
        "firstName": "Mayor18",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 18
      },{
        "firstName": "Mayor19",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 19
      },{
        "firstName": "Mayor20",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 20
      }]
    },{
      "PositionName": "Councillor",
      "NumberNeeded": 10,
      "candidates": [{
        "firstName": "Councillor1",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 21
      },{
        "firstName": "Councillor2",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 22
      },{
        "firstName": "Councillor3",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 23
      },{
        "firstName": "Councillor4",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 24
      },{
        "firstName": "Councillor5",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 25
      },{
        "firstName": "Councillor6",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 26
      },{
        "firstName": "Councillor7",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 27
      },{
        "firstName": "Councillor8",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 28
      },{
        "firstName": "Councillor9",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 29
      },{
        "firstName": "Councillor10",
        "lastName": "lastName",
        "organization": "organization",
        "candidateId": 30
      }]
    }];

    let selectedCandidateIds = new Set();
    selectedCandidateIds.add(4);
    selectedCandidateIds.add(16);
    selectedCandidateIds.add(22);
    selectedCandidateIds.add(23);
    selectedCandidateIds.add(29);

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
    let logo;
    let checkmark;

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

    let addImageOnPDF = (image, size, imageX, imageY) => {
      // auto scales image
      const imageData = this.doc.getImageProperties(image);
      const imageRatio = imageData.width / imageData.height;
      const pdfImageHeight = size;
      const pdfImageWidth = pdfImageHeight * imageRatio;

      this.doc.addImage(image, 'JPEG', imageX, imageY, pdfImageWidth, pdfImageHeight);
    }

    let createFirstPage = () => {
      addDateTimeTitle();
      
      // avoid race conditions for image placement
      const imageX = singleSpace;
      const imageY = singleSpace + doubleSpace;
      
      addImageOnPDF(logo, logoSize, imageX, imageY);
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
      if (selectedCandidateIds.has(candidate.candidateId)) {
        this.doc.setFontType("bold");
        addImageOnPDF(checkmark, 3, pageX - 4, pageY);
      }

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
      //currently using dummy race. not sure if object will actually look like this
      //someone needs to create services.
      dummyRaces.forEach(race => {
        const candidateList = race["candidates"];
        //const candidateList = pdfData["candidates"]
        let candidatesSelected = 0;
        
        newPage();
        
        candidateList.forEach(candidate => {
          candidatesSelected += selectedCandidateIds.has(candidate.candidateId) ? 1 : 0;
        });

        this.doc.setFontSize(headerFontSize);
        const raceTitle = race.PositionName + ": " + candidatesSelected + " of " + race.NumberNeeded;
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

      });
    }
    
    let createBallotPages = () => {
      //need APIs to create this.

      //newPage();
    }

    let p1 = new Promise((resolve, reject) => {
      this.getBase64Image(pdfData["electionInfo"].LogoURL, resolve);
    });

    let p2 = new Promise((resolve, reject) => {
      this.getBase64Image("https://banner2.kisspng.com/20180315/djw/kisspng-check-mark-computer-icons-clip-art-green-tick-mark-5aab1c5116d0a0.2098334515211633450935.jpg", resolve);
    });

    Promise.all([p1, p2]).then((image) => {
      logo = image[0];
      checkmark = image[1];
      createFirstPage();
      createRacePages();
      createBallotPages();

      this.doc.save(fileName);
    });
  }
}
