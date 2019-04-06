import { Component } from '@angular/core';
import { ThemeService } from '../services/theme.service';
import { Image } from '../models/image';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.less']
})
export class NavMenuComponent {
  isExpanded = false;

  logo: Image = {
    description: '',
    format: '',
    id: 'Logo',
    themeName: '',
    type: '',
    value: ''
  };
  
  constructor(private themeService: ThemeService,) {
    this.logo = this.themeService.getImage(this.logo.id);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
