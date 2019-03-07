import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from 'src/app/services/translate.service';

@Pipe({
  name: 'translate',
  pure: false
})
export class TranslatePipe implements PipeTransform {
  constructor(private translate: TranslateService) { }

  transform(param: string): any {
    var params = param.split(".");
    var component = params[0];
    var key = params[1];
    if (this.translate.data[component]) {
      return this.translate.data[component][key] || key;
    } else {
      console.error("Please check language resource files");
      return key;
    }
  }
}
