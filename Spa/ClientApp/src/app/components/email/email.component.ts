import { Component, OnInit } from "@angular/core";
import { EmailService } from "src/app/services/email.service";
import { JsonLinkerService } from 'src/app/services/json-linker.service';

const regexp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

@Component({
  selector: "app-email",
  templateUrl: "./email.component.html",
  styleUrls: ["./email.component.less"]
})
export class EmailComponent implements OnInit {
  emailaddress: string;
  subject: string;
  message: string;
  invalidEmail: boolean = false;
  noEmail: boolean = false;
  defaultMessage: string;
  defaultSubject: string;

  constructor(private emailService: EmailService, private _json : JsonLinkerService) { }

  ngOnInit() {
    this.parseDefaultEmail();
  }

  parseDefaultEmail() {
    this._json.getEmailJSON().then(data => {
      this.setQueryOptionsData(data);
    })
  }

  setQueryOptionsData(data: any) {
    this.defaultSubject = data.default.subject;
    this.defaultMessage = data.default.message;
  }

  sendReminderEmail() {
    this.noEmail = false;

    if (this.emailaddress == null || this.emailaddress.length == 0) {
      this.noEmail = true;
    } else if (regexp.test(this.emailaddress)) {

      if (this.subject == null || this.subject.length == 0) {
        // Send email using default settings
        this.emailService.sendReminderEmail(this.emailaddress, this.defaultSubject, this.defaultMessage);
      } else {
        // Send email using provided subject and message
        this.emailService.sendReminderEmail(this.emailaddress, this.subject, this.message);
      }
    } else {
      this.invalidEmail = true;
    }
  }
}
