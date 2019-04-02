import { Component, OnInit } from '@angular/core';
import { JSONParserService } from 'src/app/services/jsonparser.service';
import { Candidate } from 'src/app/models/candidate';
import { Race } from 'src/app/models/Race';
import { CandidateService } from '../services/candidate.service';

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.less']
})
export class SelectionComponent implements OnInit {
  public candidates: Candidate[] = [];
  public races: Race[] = [];
  //STEP1
  step1 = "";
  step1description = "";


  //STEP2
  step2 = "";
  step2description = "";


  //Question 1
  q0 = "";
  q0desc = "";
  q0total = "";
  //Program 1
  q0i0program = "";
  q0i0amount = "";
  q0i0desc = "";
  //Program 2
  q0i1program = "";
  q0i1amount = "";
  q0i1desc = "";
  //Program 3
  q0i2program = "";
  q0i2amount = "";
  q0i2desc = "";
  q0ans = "Unanswered";

  //Question 2
  q1 = "";
  q1desc = "";
  q1total = "";
  //Program 1
  q1i0program = "";
  q1i0amount = "";
  q1i0desc = "";
  //Program 2
  q1i1program = "";
  q1i1amount = "";
  q1i1desc = "";
  //Program 3
  q1i2program = "";
  q1i2amount = "";
  q1i2desc = "";
  q1ans = "Unanswered";

  //Question 3
  q2 = "";
  q2desc = "";
  q2total = "";
  //Program 1
  q2i0program = "";
  q2i0amount = "";
  q2i0desc = "";
  //Program 2
  q2i1program = "";
  q2i1amount = "";
  q2i1desc = "";
  q2ans = "Unanswered";

  //STEP4
  step4 = "";


  jsonData: any;
  constructor(private dataFinder: JSONParserService, private _svc: CandidateService) { }

  ngOnInit() {
    this.parseDefaultEmail();
    this.getRaces();
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
        console.log(r.show);
      }
    } else {
      for (let r of this.races) {
        if (val != r.positionName) {
          r.show = "false";
        } else {
          r.show = "true";
        }
        console.log(r.show);
     }
    }
  }

  SetQueryOptionsData(data: any) {
    this.jsonData = data;
    console.log(this.jsonData);
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
    //Q0
    this.q0 = this.jsonData.default.questions.q0.title;
    this.q0desc = this.jsonData.default.questions.q0.description;
    this.q0total = this.jsonData.default.questions.q0.total;
    this.q0i0program = this.jsonData.default.questions.q0.programs.i0.program;
    this.q0i0amount = this.jsonData.default.questions.q0.programs.i0.amount;
    this.q0i0desc = this.jsonData.default.questions.q0.programs.i0.description;
    this.q0i1program = this.jsonData.default.questions.q0.programs.i1.program;
    this.q0i1amount = this.jsonData.default.questions.q0.programs.i1.amount;
    this.q0i1desc = this.jsonData.default.questions.q0.programs.i1.description;
    this.q0i2program = this.jsonData.default.questions.q0.programs.i2.program;
    this.q0i2amount = this.jsonData.default.questions.q0.programs.i2.amount;
    this.q0i2desc = this.jsonData.default.questions.q0.programs.i2.description;
    //Q1
    this.q1 = this.jsonData.default.questions.q1.title;
    this.q1desc = this.jsonData.default.questions.q1.description;
    this.q1total = this.jsonData.default.questions.q1.total;
    this.q1i0program = this.jsonData.default.questions.q1.programs.i0.program;
    this.q1i0amount = this.jsonData.default.questions.q1.programs.i0.amount;
    this.q1i0desc = this.jsonData.default.questions.q1.programs.i0.description;
    this.q1i1program = this.jsonData.default.questions.q1.programs.i1.program;
    this.q1i1amount = this.jsonData.default.questions.q1.programs.i1.amount;
    this.q1i1desc = this.jsonData.default.questions.q1.programs.i1.description;
    this.q1i2program = this.jsonData.default.questions.q1.programs.i2.program;
    this.q1i2amount = this.jsonData.default.questions.q1.programs.i2.amount;
    this.q1i2desc = this.jsonData.default.questions.q1.programs.i2.description;
    //Q2
    this.q2 = this.jsonData.default.questions.q2.title;
    this.q2total = this.jsonData.default.questions.q2.total;
    this.q2desc = this.jsonData.default.questions.q2.description;
    this.q2i0program = this.jsonData.default.questions.q2.programs.i0.program;
    this.q2i0amount = this.jsonData.default.questions.q2.programs.i0.amount;
    this.q2i0desc = this.jsonData.default.questions.q2.programs.i0.description;
    this.q2i1program = this.jsonData.default.questions.q2.programs.i1.program;
    this.q2i1amount = this.jsonData.default.questions.q2.programs.i1.amount;
    this.q2i1desc = this.jsonData.default.questions.q2.programs.i1.description;
  }
    populateReview() {
      this.step4 = this.jsonData.default.step4;

    }
}
