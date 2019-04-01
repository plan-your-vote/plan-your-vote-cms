import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule, MatCardModule, MatDialogModule, MatInputModule, MatTableModule,
  MatSnackBarModule, MatToolbarModule, MatMenuModule, MatIconModule, MatProgressSpinnerModule,
  MatSortModule, MatRippleModule, MatExpansionModule } from '@angular/material';
import { ShareButtonsModule } from '@ngx-share/buttons';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

// services
import { TranslateService } from './services/translate.service';

// pipes
import { TranslatePipe } from './pipes/translate.pipe';

// components
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CandidateItemComponent } from './components/candidate-item/candidate-item.component';
import { CandidateListComponent } from './components/candidate-list/candidate-list.component';
import { MapComponentComponent } from './components/map-component/map-component.component';
import { EmailComponent } from './components/email/email.component';
import { FooterMenuComponent } from './footer-menu/footer-menu.component';
import { IcsfileComponent } from './components/icsfile/icsfile.component';
import { JSONParserService } from './services/jsonparser.service';
import { HttpModule } from '@angular/http';
import { SelectionComponent } from './selection/selection.component';


export function setupTranslateFactory(service: TranslateService): Function {
  return () => service.use('en');
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TranslatePipe,
    CandidateItemComponent,
    CandidateListComponent,
    MapComponentComponent,
    EmailComponent,
    FooterMenuComponent,
    IcsfileComponent,
    SelectionComponent
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    HttpModule,
    AppRoutingModule,
    FormsModule,
    MatListModule,
    MatIconModule,
    MatToolbarModule,
    MatButtonModule, 
    MatCardModule,
    MatExpansionModule,
    MatInputModule,
    MatDialogModule,
    MatTableModule,
    MatMenuModule,
    MatIconModule,
    MatRippleModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    ShareButtonsModule
  ],
  providers: [
    TranslateService,
    {
      provide: APP_INITIALIZER,
      useFactory: setupTranslateFactory,
      deps: [TranslateService],
      multi: true
    },
    JSONParserService
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
