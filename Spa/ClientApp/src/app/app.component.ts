import { Component, OnInit, NgModule } from '@angular/core';
import { ThemeService } from './services/theme.service';
import { FormsModule } from '@angular/forms';

const THEME_BASE_PATH = './assets/css';
const THEME_DEFAULT = '/default.css';
const THEME_MAPLE = '/maple.css';
const THEME_SNOWDROP = '/snowdrop.css';

@NgModule({
  imports: [FormsModule]
})
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  selectedCssFilepath: string;
  title = 'ClientApp';
  constructor(private themeService: ThemeService
  ) {}

  ngOnInit(): void {
    this.themeService.getUserSelection().then(themeName => {
      this.chooseCss(themeName);
    });
  }

  chooseCss(option: string): void {
    switch (option) {
      case 'Maple':
        this.selectedCssFilepath = THEME_MAPLE;
        break;
      case 'Snowdrop':
        this.selectedCssFilepath = THEME_SNOWDROP;
        break;
      default:
        this.selectedCssFilepath = THEME_DEFAULT;
        break;
    }

    this.themeService.document
      .getElementById('theme')
      .setAttribute('href', `${THEME_BASE_PATH}${this.selectedCssFilepath}`);
  }
}
