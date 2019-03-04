import { Component, OnInit } from '@angular/core';

import { ElectionService } from '../services/election.service';
import { Election } from '../models/election';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  data: Election[] = [];
  currentElection: Election;
  index: number;
  
  constructor(
    private electionApi: ElectionService
  ) {
    this.index = 0;
    this.electionApi.getElections()
      .subscribe(res => {
        this.data = res;
      });
   }

  ngOnInit(): void {
      this.nextElection();
  }

  public nextElection(): void {
    this.currentElection = this.data[this.index];
    if (this.index != this.data.length-1) {
      this.index++;
    } else {
      this.index = 0;
    }
  }
}
