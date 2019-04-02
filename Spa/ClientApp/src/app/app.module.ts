import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
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
import { JSONParserService } from './services/jsonparser.service';

// pipes
import { TranslatePipe } from './pipes/translate.pipe';

// components
import { CandidateItemComponent } from './components/candidate-item/candidate-item.component';
import { CandidateListComponent } from './components/candidate-list/candidate-list.component';
import { MapComponentComponent } from './components/map-component/map-component.component';
import { EmailComponent } from './components/email/email.component';
import { IcsfileComponent } from './components/icsfile/icsfile.component';
import { ImageTopComponent } from './components/card/image-top/image-top.component';
import { ImageOverlayComponent } from './components/card/image-overlay/image-overlay.component';
import { SelectionComponent } from './selection/selection.component';

import { FooterMenuComponent } from './footer-menu/footer-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';


export function setupTranslateFactory(service: TranslateService): Function {
  return () => service.use('en');
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    TranslatePipe,
    CandidateItemComponent,
    CandidateListComponent,
    MapComponentComponent,
    EmailComponent,
    FooterMenuComponent,
    IcsfileComponent,
    ImageTopComponent,
    ImageOverlayComponent,
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
