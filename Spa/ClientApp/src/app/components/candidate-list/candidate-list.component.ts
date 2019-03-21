import { Component, OnInit } from '@angular/core';
import { CandidateService } from '../../services/candidate.service';
import { Candidate } from 'src/app/models/candidate';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  styleUrls: ['./candidate-list.component.less']
})
export class CandidateListComponent implements OnInit {
  title = 'candidates';

  public candidates: Candidate[] = [];

  constructor(private _svc: CandidateService) { }
  getCandidates(): void {
    this._svc.getCandidates().subscribe(data => (this.candidates = data));
  }

  ngOnInit() {
    if (!localStorage.getItem('candidates')) {
      this.getCandidates();
    } else {
      this.candidates = JSON.parse(localStorage.getItem('candidates'));
    }

  }

  onSelect(i, c: Candidate) {
    let candidate_btn = document.getElementById('candidate-btn' + i);
    if (!c.selected) {
      c.selected = true;
      localStorage.setItem('candidates', JSON.stringify(this.candidates));
      candidate_btn.classList.add("btn-danger");
      candidate_btn.classList.remove("btn-success");
    } else {
      c.selected = false;
      localStorage.setItem('candidates', JSON.stringify(this.candidates));
      candidate_btn.classList.remove("btn-danger");
      candidate_btn.classList.add("btn-success");
    }

  }



}
