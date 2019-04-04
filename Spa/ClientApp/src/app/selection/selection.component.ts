import { Component, OnInit } from '@angular/core';
import { JSONParserService } from 'src/app/services/jsonparser.service';
import { Candidate } from 'src/app/models/candidate';
import { Race } from 'src/app/models/Race';
import { CandidateService } from '../services/candidate.service';
import { BallotIssue } from '../models/BallotIssue';
import { CandidateRace } from '../models/CandidateRace';

@Component({
  selector   : 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls  : ['./selection.component.less']
})
export class SelectionComponent implements OnInit {
  public selectedRaces: Race[] = [];
  public races: Race[]         = [];
  public issues: BallotIssue[] = [];
  //STEP1
  step1            = "";
  step1description = "";

  //STEP2
  step2            = "";
  step2description = "";

  //STEP4
  step4 = "";


  jsonData: any;
  constructor(private dataFinder: JSONParserService, private _svc: CandidateService) { }

  ngOnInit() {
    this.parseDefaultEmail();
    this.getRaces();
    this.getIssues();
  }
  parseDefaultEmail() {
    this.dataFinder.getJSONDataAsync("./assets/data/selection-content.json").then(data => {
      this.SetQueryOptionsData(data);
    })
  }

  getRaces(): void {
    this._svc.getRaces().subscribe(data => {
      this.races = data;
      for (let r of this.races) {
        r.show     = "true";
        r.selected = [];
      }
      if (localStorage.getItem('selectedRaces')) {
        this.selectedRaces = JSON.parse(localStorage.getItem('selectedRaces'));
        this.selectedRaces.forEach(s => {
          const r: Race = this.races.find(element => element.raceId === s.raceId);
          if(r) {
            r.selected = s.selected;
            s.candidateRaces.forEach(scr => {
              const cr: CandidateRace = r.candidateRaces.find(element => element.candidateId === scr.candidateId);
              if(cr && scr.candidate.selected === true) {
                cr.candidate.selected = true;
              }
            });
          }
        });
      }
    });
  }

  getIssues(): void {
    this._svc.getBallotIssues().subscribe(data => {
      this.issues = data;
      for (let r of this.issues) {
        r.answer = "Unanswered";
      }
    });
  }

  onSelect(c: Candidate, r: Race) {
    if (!c.selected) {
      c.selected = true;
      r.selected.push(c);
      var unique: Boolean = true;
      for(let s of this.selectedRaces) {
        if (s.raceId == r.raceId)
          unique = false;
      }
      if(unique)
        this.selectedRaces.push(r);
    } else {
      c.selected = false;
      r.selected = r.selected.filter(function (e) { return e.candidateId !== c.candidateId });
      // remove race data from localStorage if no candidates are selected
      if(r.selected.length == 0) {
        this.selectedRaces.splice(this.selectedRaces.indexOf(r), 1);
      }
    }
    localStorage.setItem('selectedRaces', JSON.stringify(this.selectedRaces));
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
    this.step1            = this.jsonData.default.step1;
    this.step1description = this.jsonData.default.step1description;
  }
  populateStepTwo() {
    this.step2            = this.jsonData.default.step2;
    this.step2description = this.jsonData.default.step2description;
  }
    populateReview() {
      this.step4 = this.jsonData.default.step4;
    }
}
