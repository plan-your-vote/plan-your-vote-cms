import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-icsfile',
  templateUrl: './icsfile.component.html',
  styleUrls: ['./icsfile.component.less']
})
export class IcsfileComponent implements OnInit {
  selectedDate = new Date();
  data:Array<Object> = [
      {id: 0, date: "04/15/2019"},
      {id: 1, date: "04/16/2019"},
      {id: 2, date: "04/17/2019"}
  ];

  constructor() { }

  ngOnInit() {
    
  }


}
