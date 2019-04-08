import { CandidateRace } from './../../models/CandidateRace';
import { Component, OnInit, Input } from "@angular/core";
import { Candidate } from "src/app/models/candidate";
import { Race } from "src/app/models/Race";

@Component({
  selector   : "app-candidate-table",
  templateUrl: "./candidate-table.component.html",
  styleUrls  : ["./candidate-table.component.less"]
})
export class CandidateTableComponent implements OnInit {
  @Input() race                : Race;
  public selectedCandidateRaces: CandidateRace[] = [];

  constructor() {}

  ngOnInit() {
    if (localStorage.getItem('selectedCandidateRaces')) {
      this.selectedCandidateRaces = JSON.parse(localStorage.getItem('selectedCandidateRaces'));
      this.selectedCandidateRaces.forEach(cr => {

        if(this.race.raceId === cr.raceId && cr.candidate.selected === true) {

          // show candidate in step 4
          // note: bugged if you refresh at step 4, will need to click step one and navigate back
          // to step 4 for it to work. May be an async issue.
          this.race.selected.push(cr.candidate);

          // change button display for step 1
          var scr                    = this.race.candidateRaces.find(element => element.candidateId === cr.candidateId);
              scr.candidate.selected = true;
        }

      });
    }
  }

  onSelect(c: Candidate, r: Race) {
    
    // include candidates from other races
    if(localStorage.getItem('selectedCandidateRaces')) {
      var temp: CandidateRace[] = JSON.parse(localStorage.getItem('selectedCandidateRaces'));
    
      this.selectedCandidateRaces = temp.concat(this.selectedCandidateRaces);
      for(var i = 0; i < this.selectedCandidateRaces.length; ++i) {
        for(var j = i + 1; j < this.selectedCandidateRaces.length; ++j) {
            if(this.selectedCandidateRaces[i].candidateId === this.selectedCandidateRaces[j].candidateId)
                this.selectedCandidateRaces.splice(j--, 1);
        }
      }
    }
    
    if (!c.selected) {
      c.selected = true;
      r.selected.push(c);
      this.selectedCandidateRaces.push(r.candidateRaces.find(element => element.candidateId === c.candidateId));
    } else {
      c.selected = false;
      r.selected = r.selected.filter(function (e) { return e.candidateId !== c.candidateId });
      // the following has a bug: selecting candidate with id 3 for mayor, then councillor, then deselecting him for mayor will
      // remove both from selectedCandidateRaces. For some reason, deselecting him for councillor then mayor works fine.
      this.selectedCandidateRaces = this.selectedCandidateRaces.filter(function (e) { return e.candidateId !== c.candidateId || e.raceId !== r.raceId });
    }
    
    localStorage.setItem('selectedCandidateRaces', JSON.stringify(this.selectedCandidateRaces));
  }
}
