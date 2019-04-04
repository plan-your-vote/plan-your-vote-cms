import { Component, OnInit, Input } from "@angular/core";
import { Candidate } from "src/app/models/candidate";
import { Race } from "src/app/models/Race";

@Component({
  selector: "app-candidate-table",
  templateUrl: "./candidate-table.component.html",
  styleUrls: ["./candidate-table.component.less"]
})
export class CandidateTableComponent implements OnInit {
  @Input() race: Race;

  public candidates: Candidate[] = [];

  constructor() {}

  ngOnInit() {}

  onSelect(c: Candidate, r: Race) {
    if (!c.selected) {
      c.selected = true;
      localStorage.setItem("candidates", JSON.stringify(this.candidates));
      r.selected.push(c);
    } else {
      c.selected = false;
      localStorage.setItem("candidates", JSON.stringify(this.candidates));
      r.selected = r.selected.filter(function(e) {
        return e.candidateId !== c.candidateId;
      });
    }
  }
}
