import { Component, OnInit } from '@angular/core';
import { JSONParserService } from 'src/app/services/jsonparser.service';
import { Candidate } from 'src/app/models/candidate';
import { Race } from 'src/app/models/Race';
import { CandidateService } from '../services/candidate.service';
import { BallotIssue } from '../models/BallotIssue';

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.less']
})
export class SelectionComponent implements OnInit {
  public candidates: Candidate[] = [];
  public races: Race[] = [];
  public issues: BallotIssue[] = [];
  //STEP1
  step1 = "";
  step1description = "";

  //STEP2
  step2 = "";
  step2description = "";

  //STEP4
  step4 = "";


  jsonData: any;
  constructor(private dataFinder: JSONParserService, private _svc: CandidateService) { }

  ngOnInit() {
    this.parseDefaultEmail();
    this.getRaces();
    this.getIssues();
    console.log(this.races);
  }
  parseDefaultEmail() {
    this.dataFinder.getJSONDataAsync("./assets/data/selection-content.json").then(data => {
      this.SetQueryOptionsData(data);
    })
  }

  getRaces(): void {
    this._svc.getRaces().subscribe(data => {
      this.races = data;
      console.log(this.races);
      for (let r of this.races) {
        r.show = "true";
        r.selected = [];
      }
    });
  }

  getIssues(): void {
    this._svc.getBallotIssues().subscribe(data => {
      this.issues = data;
      console.log(this.issues);
      for (let r of this.issues) {
        r.answer = "Unanswered";
      }
    });
  }

  onSelect(c: Candidate, r: Race) {
    if (!c.selected) {
      c.selected = true;
      localStorage.setItem('candidates', JSON.stringify(this.candidates));
      r.selected.push(c);
    } else {
      c.selected = false;
      localStorage.setItem('candidates', JSON.stringify(this.candidates));
      r.selected = r.selected.filter(function (e) { return e.candidateId !== c.candidateId });
    }
    console.log(this.races);
  }

  filterRaces(val: any) {
    if (val == "All Races") {
      for (let r of this.races) {
        r.show = "true";
      }
    } else {
      for (let r of this.races) {
        if (val != r.positionName) {
          r.show = "false";
        } else {
          r.show = "true";
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
}
