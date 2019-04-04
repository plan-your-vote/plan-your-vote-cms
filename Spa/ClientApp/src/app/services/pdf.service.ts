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
   * @param pdfData PDF data passed through app.component.ts
   * @param fileName name of the file to be saved
   */
  public pdf(pdfData: object): void {
    //Setup
    this.doc = new jsPDF();

    //Default size is 210 x 297
    const MAX_PAGE_X = 210;
    const MAX_PAGE_Y = 297;

    //Change this value to change checkmark size
    const checkmarkSize = 3;
    let checkmark;

    //Change this value to change logo size
    const logoSize = 20;
    let logo;

    //Spacing
    const smallSpace = 4;
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
      this.doc.text(pdfData["electionInfo"].name, MAX_PAGE_X/2, dateTimeTitleY, {align:"center"});
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

    //TODO: Make first page grab info dynamically
    let createFirstPage = () => {
      addDateTimeTitle();
      
      if (logo) {
        addImageOnPDF(logo, logoSize, pageX, pageY + singleSpace);
      }
      pageY += largeSpace;

      this.doc.setFontSize(titleFontSize);
      this.doc.text("My Summary", MAX_PAGE_X/2, pageY, {align:"center"});
      if (logo) {
        pageY += logoSize;
      }

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

    let createCandidateCard = (candidate, selected) => {
      //This adds the checkmark
      if (selected) {
        this.doc.setFontType("bold");
        addImageOnPDF(checkmark, checkmarkSize, pageX - smallSpace, pageY - checkmarkSize);
      }

      //This prints the candidate name. Separates first and last name if name too long.
      this.doc.setFontSize(defaultFontSize);
      if (candidate.lastName.length + candidate.firstName.length > 20) {
        this.doc.text(candidate.lastName.toUpperCase() + ",", pageX, pageY);
        pageY += singleSpace;
        this.doc.text(candidate.firstName, pageX, pageY);
      } else {
        let candidateName = candidate.lastName.toUpperCase() + ", " + candidate.firstName;
        this.doc.text(candidateName, pageX, pageY);
      }

      //This prints the candidate organization
      if (candidate.organization) {
        pageY += singleSpace;
        this.doc.setFontSize(footerFontSize);
        this.doc.text(candidate.organization.toUpperCase(), pageX, pageY);
      }

      //This returns the font size to normal after it is bolded from selection
      this.doc.setFontType("normal");

      pageY += doubleSpace + singleSpace;
    }

    let createRacePages = () => {
      const races = pdfData["races"];
      races.forEach(race => {
        const candidateRaces = race["candidateRaces"];
        const candidatesSelected = race["selected"] || [];
        
        newPage();
        
        let selectedCandidateIds = new Set();

        candidatesSelected.forEach(candidate => {
          if (candidate) {
            selectedCandidateIds.add(candidate.candidateId);
          }
        });

        //Race Title
        this.doc.setFontSize(headerFontSize);
        const raceTitle = race.positionName + ": " + candidatesSelected.length + " of " + race.numberNeeded;
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
  
        //Adds candidates. If candidate card is too long, will go to next column.
        //On the 4th column, will create a new page instead.
        candidateRaces.forEach(candidateRace => {
          const candidate = candidateRace.candidate;
          let selected = selectedCandidateIds.has(candidate.candidateId);
          if (candidate) {
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
            createCandidateCard(candidate, selected);
          }
        });
      });
    }
    
    //TODO: Create new page when ballot issues get too long.
    let createBallotPages = () => {
      const ballotIssues = pdfData["ballotIssues"];
      if (ballotIssues) {
        newPage();
        addDateTimeTitle();

        let issueNumber = 0;
        ballotIssues.forEach(issue => {
          issueNumber++;

          //This adds issue header
          this.doc.setFontSize(headerFontSize);
          this.doc.setFontType("bold");
          const ballotTitle = issueNumber + ": " + issue.ballotIssueTitle;
          const splitTitle = this.doc.splitTextToSize(ballotTitle, MAX_PAGE_X - largeSpace);
          this.doc.text(splitTitle, pageX, pageY);
          this.doc.setFontType("normal");
          let numberOfLines = splitTitle.length;
          console.log(splitTitle);
          console.log(numberOfLines);

          //Value of 5.6 from trial and error
          pageY += doubleSpace + (5.6 * (numberOfLines - 1));
          
          //Horizontal line
          this.doc.line(pageX, pageY, MAX_PAGE_X - pageX, pageY);
          pageY += doubleSpace;
        });
      }
    }

    //TODO
    let p1 = new Promise((resolve, reject) => {
      const electionImages = JSON.parse(pdfData["electionBanner"])
      let imageFormat;
      let electionLogo = electionImages.some(img => {
        if (img.id === "Logo") {
          imageFormat = img.format;
          return img.value;
        }
      })
      if (imageFormat === "PNG" || imageFormat === "JPG" || imageFormat === "JPEG" || imageFormat === "GIF") {
        this.getBase64Image(electionLogo, resolve);
      } else if (imageFormat === "SVG") {
        //TODO
        return false;
      } else {
        //reject
      }
    });
    
    //TODO remove
    let p3 = new Promise((resolve, reject) => {
      this.getBase64Image("https://www.worldatlas.com/webimage/flags/countrys/zzzflags/calarge.gif", resolve);
    });

    let p2 = new Promise((resolve, reject) => {
      this.getBase64Image("https://banner2.kisspng.com/20180315/djw/kisspng-check-mark-computer-icons-clip-art-green-tick-mark-5aab1c5116d0a0.2098334515211633450935.jpg", resolve);
    });

    Promise.all([p3, p2]).then(image => {
      logo = image[0];
      checkmark = image[1];
      createFirstPage();
      createRacePages();
      createBallotPages();

      let fileName = pdfData["electionInfo"].name.replace(/[\W_]+/g," ")
      this.doc.save(fileName);
    });
  }
}
