import { Injectable } from '@angular/core';

import email from 'src/assets/data/email.json';
import selectionContent from 'src/assets/data/selection-content.json';

@Injectable({
  providedIn: 'root'
})
export class JsonLinkerService {

  constructor() { }

  getEmailJSON () {
    return new Promise<{}>((resolve, reject) => {
      const data = Object.assign({}, email || {});
      return resolve(data);
    })    
  }

  getSelectionContent () {
    return new Promise<{}>((resolve, reject) => {
      const data = Object.assign({}, selectionContent || {});
      return resolve(data);
    })
  }
}
