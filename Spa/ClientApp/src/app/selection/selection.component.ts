import { Component, OnInit } from "@angular/core";

import { BallotIssue } from "../models/BallotIssue";
import { Candidate } from "src/app/models/candidate";
import { Election } from "src/app/models/election";
import { Race } from "src/app/models/Race";

import { CandidateService } from "../services/candidate.service";
import { ElectionService } from "src/app/services/election.service";
import { JSONParserService } from "src/app/services/jsonparser.service";
import { PdfService } from "src/app/services/pdf.service";

@Component({
  selector: "app-selection",
  templateUrl: "./selection.component.html",
  styleUrls: ["./selection.component.less"]
})
export class SelectionComponent implements OnInit {
  candidates: Candidate[];
  currentElection: Election;
  elections: Election[] = [];
  data: Election[] = [];
  index: number;

  races: Race[] = [];
  issues: BallotIssue[] = [];

  //STEP1
  step1title = "";
  step1description = "";

  //STEP2
  step2title = "";
  step2description = "";

  //STEP4
  step4title = "";

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
      .getJSONDataAsync("./assets/data/selection-content.json")
      .then(data => {
        this.SetQueryOptionsData(data);
      });
  }

  SetQueryOptionsData(data: any) {
    this.jsonData = data;
    this.populateStepOne();
    this.populateStepTwo();
    this.populateReview();
  }

  populateStepOne() {
    this.step1title = this.jsonData.default.step1title;
    this.step1description = this.jsonData.default.step1description;
  }
  populateStepTwo() {
    this.step2title = this.jsonData.default.step2title;
    this.step2description = this.jsonData.default.step2description;
  }
  populateReview() {
    this.step4title = this.jsonData.default.step4title;
  }

  getRaces(): void {
    this._svc.getRaces().subscribe(data => {
      this.races = data;
      for (let race of this.races) {
        race.show = "true";
        race.selected = [];
      }
    });
  }

  getIssues(): void {
    this._svc.getBallotIssues().subscribe(data => {
      this.issues = data;
      for (let issue of this.issues) {
        issue.answer = "Unanswered";
      }
    });
  }

  /**
   * Attached to 'Try PDF' button.
   * Currently passing all candidates.
   * Needs to be divided by races instead of candidates.
   * Needs to implement candidate selection after Greg implements.
   */
  generatePdf() {
    let selectedCandidateIds = new Set();

    if (localStorage.getItem("candidates")) {
      let selectedCandidates = JSON.parse(localStorage.getItem("candidates"));

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
      this.currentElection.VoteTitle.replace(/[\W_]+/g, " ")
    );
  }
}
