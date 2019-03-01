# PDF Generator
This document let you to set up Angular PDF Generator on the project and how to use it.

## Table of Contents
- [Angular Library](#angular-library)
- [Set Up](#set-up)

## Angular Library
The project will be using `angular-pdf-generator` which resides on npm.

## Set Up
- Run `$ npm install angular-pdf-generator --save`
- In `App.Module`, do like following code.
```
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
 
import { AppComponent } from './app.component';
 
// Import your library
import { SampleModule } from 'angular-pdf-generator';
 
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
 
    // Specify your library as an import
    LibraryModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
```

The Guide is from this [website](https://www.npmjs.com/package/angular-pdf-generator).