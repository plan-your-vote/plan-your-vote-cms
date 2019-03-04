import { Component, OnInit } from "@angular/core";
import { EmailService } from "src/app/services/email.service";

@Component({
  selector: "app-email",
  templateUrl: "./email.component.html",
  styleUrls: ["./email.component.less"]
})
export class EmailComponent implements OnInit {
  emailaddress: string;
  subject: string;
  message: string;

  constructor(private emailService: EmailService) {}

  ngOnInit() {}

  sendReminderEmail() {
    this.emailService.sendReminderEmail(this.emailaddress, this.subject, this.message);
  }
}
