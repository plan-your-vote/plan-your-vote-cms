import { Component, OnInit } from '@angular/core';

import { BallotIssue } from '../models/BallotIssue';
import { Candidate } from 'src/app/models/candidate';
import { Election } from 'src/app/models/election';
import { Race } from 'src/app/models/Race';

import { CandidateService } from '../services/candidate.service';
import { ElectionService } from 'src/app/services/election.service';
import { JSONParserService } from 'src/app/services/jsonparser.service';
import { PdfService } from 'src/app/services/pdf.service';

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.less']
})
export class SelectionComponent implements OnInit {
  public races: Race[] = [];
  public issues: BallotIssue[] = [];
  candidates: Candidate[];
  currentElection: Election;
  elections: Election[] = [];
  data: Election[] = [];
  index: number;

  //STEP1
  step1 = '';
  step1description = '';

  //STEP2
  step2 = '';
  step2description = '';

  //STEP4
  step4 = '';

  jsonData: any;
  constructor(
    private dataFinder: JSONParserService,
    private electionApi: ElectionService,
    private _svc: CandidateService,
    private pdfService: PdfService,
    private candidatesApi: CandidateService
  ) {
    this.index = 0;

    this.electionApi.getElections().subscribe(res => {
      this.elections = res;
      this.data = res;
      this.currentElection = this.elections[this.index];

      //sets the current selection to the first one
      //TODO: do this async with a promise
      if (!this.currentElection) {
        this.currentElection = this.data[this.index];
      }
    });

    this.candidatesApi.getCandidates().subscribe(candidates => {
      this.candidates = candidates;
    });
  }

  ngOnInit() {
    this.parseDefaultEmail();
    this.getRaces();
    this.getIssues();
  }
  parseDefaultEmail() {
    this.dataFinder
      .getJSONDataAsync('./assets/data/selection-content.json')
      .then(data => {
        this.SetQueryOptionsData(data);
      });
  }

  getRaces(): void {
    this._svc.getRaces().subscribe(data => {
      this.races = data;
      for (let r of this.races) {
        r.show = 'true';
        r.selected = [];
      }
    });
  }

  getIssues(): void {
    this._svc.getBallotIssues().subscribe(data => {
      this.issues = data;
      for (let r of this.issues) {
        r.answer = 'Unanswered';
      }
    });
  }

  filterRaces(val: any) {
    if (val == 'All Races') {
      for (let r of this.races) {
        r.show = 'true';
      }
    } else {
      for (let r of this.races) {
        if (val != r.positionName) {
          r.show = 'false';
        } else {
          r.show = 'true';
        }
      }
    }
  }

  SetQueryOptionsData(data: any) {
    this.jsonData = data;
    this.populateStepOne();
    this.populateStepTwo();
    this.populateReview();
  }

  populateStepOne() {
    this.step1 = this.jsonData.default.step1;
    this.step1description = this.jsonData.default.step1description;
  }
  populateStepTwo() {
    this.step2 = this.jsonData.default.step2;
    this.step2description = this.jsonData.default.step2description;
  }
  populateReview() {
    this.step4 = this.jsonData.default.step4;
  }

  /**
   * Attached to 'Try PDF' button.
   * Currently passing all candidates.
   * Needs to be divided by races instead of candidates.
   * Needs to implement candidate selection after Greg implements.
   */
  generatePdf() {
    let selectedCandidateIds = new Set();

    if (localStorage.getItem('candidates')) {
      let selectedCandidates = JSON.parse(localStorage.getItem('candidates'));

      selectedCandidates.forEach(c => {
        selectedCandidateIds.add(c.candidateId);
      });
    }

    let pdfData: object = {
      dateTime: new Date().toLocaleString(),
      electionInfo: this.currentElection,
      candidates: this.candidates,
      selectedCandidateIds: selectedCandidateIds
    };

    this.pdfService.pdf(
      pdfData,
      this.currentElection.VoteTitle.replace(/[\W_]+/g, ' ')
    );
  }
}
