import { Component, OnInit } from "@angular/core";
import { EmailService } from "src/app/services/email.service";
import { invalid } from '@angular/compiler/src/render3/view/util';
import { JSONParserService } from 'src/app/services/jsonparser.service';

@Component({
  selector: "app-email",
  templateUrl: "./email.component.html",
  styleUrls: ["./email.component.less"]
})
export class EmailComponent implements OnInit {
  emailaddress: string;
  subject: string;
  message: string;
  invalidemail: boolean = false;
  noemail: boolean = false;
  defaultMessage: string;
  defaultSubject: string;

  constructor(private emailService: EmailService, private dataFinder: JSONParserService) {}

  ngOnInit() {
    this.parseDefaultEmail();
  }
  parseDefaultEmail() {
    this.dataFinder.getJSONDataAsync("./assets/data/email.json").then(data => {
      this.SetQueryOptionsData(data);
    })
  }

  SetQueryOptionsData(data: any) {
    this.defaultSubject = data.default.Subject;
    this.defaultMessage = data.default.Message;
  }

  sendReminderEmail() {
    //EmailRegex
    var regexp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
    this.invalidemail = false;
    this.noemail = false;

    if (this.emailaddress == null || this.emailaddress.length == 0) {
      this.noemail = true;
      //console.log("NO EMAIL");

    } else if (regexp.test(this.emailaddress)) {


      if (this.subject == null || this.subject.length == 0) {
        this.emailService.sendReminderEmail(this.emailaddress, this.defaultSubject, this.defaultMessage); //Default Message Email
        //console.log("DEFAULT EMAIL SENT");
      } else {
        this.emailService.sendReminderEmail(this.emailaddress, this.subject, this.message); //Custom Subject Line
        //console.log("CUSTOM EMAIL SENT");

      }
    } else{
      //handle error
      //send warning
      this.invalidemail = true;
      //console.log("INVALID EMAIL");
    }

  }
}
