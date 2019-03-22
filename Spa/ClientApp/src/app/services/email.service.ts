import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

const httpOptions = {
  headers: new HttpHeaders({ "Content-Type": "application/json",
   "Access-Control-Allow-Origin": "EmailPolicy"
  })
}

@Injectable({
  providedIn: "root"
})
export class EmailService {

  constructor(private http: HttpClient) { }

  sendReminderEmail(emailAddress: string, subject: string, message: string) {
    this.http
      .post<string>(
        "https://localhost:44307/api/email",
        {
          EmailAddress: emailAddress,
          Subject: subject,
          Message: message,
        },
        httpOptions
      ).subscribe();
  }
}
