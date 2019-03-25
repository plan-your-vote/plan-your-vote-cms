import { Component, OnInit } from "@angular/core";
import { EmailService } from "src/app/services/email.service";
import { invalid } from '@angular/compiler/src/render3/view/util';

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

  constructor(private emailService: EmailService) {}

  ngOnInit() {}

  sendReminderEmail() {
    //EmailRegex
    var regexp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
    this.invalidemail = false;
    this.noemail = false;

    if (this.emailaddress == null || this.emailaddress.length == 0) {
      this.noemail = true;
      console.log("NO EMAIL");

    } else if (regexp.test(this.emailaddress)) {
      this.emailService.sendReminderEmail(this.emailaddress, this.subject, this.message);
      console.log("EMAIL SENT");
    } else{
      //handle error
      //send warning
      this.invalidemail = true;
      console.log("INVALID EMAIL");
    }

  }
}
