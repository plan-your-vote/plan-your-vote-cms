import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LocalStorageComponent } from './local-storage/local-storage.component';

@NgModule({
  declarations: [
    AppComponent,
    LocalStorageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [LocalStorageComponent]
})
export class AppModule { }
