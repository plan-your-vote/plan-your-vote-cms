import { Component, OnInit } from '@angular/core';
import { CandidateService } from '../../services/candidate.service';
import { Candidate } from 'src/app/models/candidate';

@Component({
  selector   : 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  styleUrls  : ['./candidate-list.component.less']
})
export class CandidateListComponent implements OnInit {
  title = 'candidates';

  public candidates: Candidate[] = [];

  constructor(private _svc: CandidateService) {}
  getCandidates(): void {
    this._svc.getCandidates().subscribe(data => (this.candidates = data));
  }

  ngOnInit() {
    if(!localStorage.getItem('candidates')) {
      this.getCandidates();
    } else {
      this.candidates = JSON.parse(localStorage.getItem('candidates'));
    }
    alert(this.candidates[0])
  }

  onSelect(c: Candidate) {
    if(!c.selected) {
      c.selected = true;
    
      localStorage.setItem('candidates', JSON.stringify(this.candidates));
    } else {
      c.selected = false;
      localStorage.setItem('candidates', JSON.stringify(this.candidates));
    }
    
  }

  

}
