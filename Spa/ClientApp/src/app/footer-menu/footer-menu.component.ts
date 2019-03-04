import { Component, OnInit } from '@angular/core';
import { ShareService } from '@ngx-share/core';

@Component({
  selector: 'app-footer-menu',
  templateUrl: './footer-menu.component.html',
  styleUrls: ['./footer-menu.component.less']
})
export class FooterMenuComponent implements OnInit {

  constructor(public share: ShareService) { }

  ngOnInit() {
  }

}
