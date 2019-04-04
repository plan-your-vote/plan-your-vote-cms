import { Component, OnInit } from "@angular/core";

import { BallotIssue } from "../models/BallotIssue";
import { Candidate } from "src/app/models/candidate";
import { Election } from "src/app/models/election";
import { Race } from "src/app/models/Race";

import { CandidateService } from "../services/candidate.service";
import { ElectionService } from "src/app/services/election.service";
import { JSONParserService } from "src/app/services/jsonparser.service";
import { PdfService } from "src/app/services/pdf.service";
import { ActivatedRoute, ParamMap } from "@angular/router";

@Component({
  selector: "app-selection",
  templateUrl: "./selection.component.html",
  styleUrls: ["./selection.component.less"]
})
export class SelectionComponent implements OnInit {
  candidates: Candidate[];
  currentElection: Object;
  races: Race[] = [];
  issues: BallotIssue[] = [];

  currentStep: string;

  //STEP1
  step1title = "";
  step1description = "";

  //STEP2
  step2title = "";
  step2description = "";

  //STEP3
  step3title = "";
  step3description = "";

  //STEP4
  step4title = "";

  jsonData: any;
  constructor(
    private dataFinder: JSONParserService,
    private electionApi: ElectionService,
    private _svc: CandidateService,
    private pdfService: PdfService,
    private candidatesApi: CandidateService,
    private route: ActivatedRoute
  ) {
    this.electionApi.getElection().subscribe(election => {
      this.currentElection = election;
    });

    this.candidatesApi.getCandidates().subscribe(candidates => {
      this.candidates = candidates;
    });
  }

  defaultStep: string = "step1";
  ngOnInit() {
    this.parseDefaultEmail();

    this.getRaces();
    this.getIssues();

    this.route.paramMap.subscribe((params: ParamMap) => {
      this.currentStep = this.route.snapshot.params.step;
    });

    // one time default fake routing
    if (this.currentStep == null) {
      this.currentStep = this.defaultStep;
    }
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
    this.populateStepThree();
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
  populateStepThree() {
    this.step3title = this.jsonData.default.step3title;
    this.step3description = this.jsonData.default.step3description;
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
   */
  generatePdf() {
    const pdfData: object = {
      dateTime: new Date().toLocaleString(),
      electionInfo: this.currentElection,
      electionBanner: localStorage.images,
      races: this.races,
      ballotIssues: this.issues
    };
    
    this.pdfService.pdf(pdfData);
  }
}
