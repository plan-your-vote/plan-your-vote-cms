import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-step3',
  templateUrl: './step3.component.html',
  styleUrls: ['./step3.component.less']
})
export class Step3Component implements OnInit {
  @Input() public step3title: string;
  @Input() public step3description: string;

  constructor() { }

  ngOnInit() {
  }

}
