import { Component, OnInit } from '@angular/core';

@Component({
  selector   : 'app-local-storage',
  templateUrl: './local-storage.component.html',
  styleUrls  : ['./local-storage.component.css'],
  template   : `
    <button (click)="store()">Store value locally</button>
    <br />
    <button (click)="clear()">Clear local storage</button>
    <br />
    {{clickMessage}}`
})
export class LocalStorageComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    if(localStorage.length == 0) {
      this.clickMessage = '';
    } else {
      this.clickMessage = localStorage.getItem('key');
    }
  }

  clickMessage = '';

  store() {
    this.clickMessage = 'Stored!';
    localStorage.setItem('key', 'Fetched value from local storage!');
  }

  clear() {
    this.clickMessage = 'Cleared!';
    localStorage.clear();
  }

}
