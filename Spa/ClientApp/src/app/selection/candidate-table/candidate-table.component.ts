import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-candidate-table',
  templateUrl: './candidate-table.component.html',
  styleUrls: ['./candidate-table.component.less'],
  inputs: ['race']
})
export class CandidateTableComponent implements OnInit {

  @Input() race;

  constructor() { }

  ngOnInit() {
  }

}
