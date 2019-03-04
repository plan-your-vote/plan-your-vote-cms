import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule, MatCardModule, MatDialogModule, MatInputModule, MatTableModule,
  MatSnackBarModule, MatToolbarModule, MatMenuModule, MatIconModule, MatProgressSpinnerModule,
  MatSortModule, MatRippleModule } from '@angular/material';

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
    MapComponentComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    MatListModule,
    MatIconModule,
    MatToolbarModule,
    MatButtonModule, 
    MatCardModule,
    MatInputModule,
    MatDialogModule,
    MatTableModule,
    MatMenuModule,
    MatIconModule,
    MatRippleModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatSnackBarModule
  ],
  providers: [
    TranslateService,
    {
      provide: APP_INITIALIZER,
      useFactory: setupTranslateFactory,
      deps: [TranslateService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
