import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "EmailPolicy"
  })
}

@Injectable({
  providedIn: "root"
})
export class EmailService {

  constructor(private http: HttpClient) { }

  sendReminderEmail(emailAddress: string, subject: string, message: string) {
    let url = environment.emailUrl;

    this.http
      .post<string>(url,
        {
          EmailAddress: emailAddress,
          Subject: subject,
          Message: message,
        },
        httpOptions
      ).subscribe();
  }
}
