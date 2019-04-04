import { Component, OnInit, Input } from '@angular/core';
import { Candidate } from 'src/app/models/candidate';
import { Race } from 'src/app/models/Race';

@Component({
  selector: 'app-candidate-table',
  templateUrl: './candidate-table.component.html',
  styleUrls: ['./candidate-table.component.less'],
  inputs: ['race']
})
export class CandidateTableComponent implements OnInit {
  
  @Input() race;

  public candidates: Candidate[] = [];

  constructor() { }

  ngOnInit() {
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
    console.log(this.race);
  }


}
