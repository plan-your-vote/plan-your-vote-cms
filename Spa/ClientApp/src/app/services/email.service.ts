import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class EmailService {
  
  constructor(private http: HttpClient) {}

  // Creates errors in promise
  sendReminderEmail(emailAddress: string, subject: string, message: string) {
    this.http
      .post<string>(
        "https://localhost:44307/api/email",
        {
          EmailAddress: emailAddress,
          Subject: subject,
          Message: message,
        },
        { headers: new HttpHeaders({ "Content-Type": "application/json", "Access-Control-Allow-Origin": "EmailPolicy"}) }
      )
      .toPromise()
      .catch();
  }
}
