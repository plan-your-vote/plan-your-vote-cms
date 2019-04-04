import { Component, OnInit, Input } from "@angular/core";
import { Race } from "src/app/models/Race";

@Component({
  selector: "app-step1",
  templateUrl: "./step1.component.html",
  styleUrls: ["./step1.component.less"]
})
export class Step1Component implements OnInit {
  @Input() public step1title: string;
  @Input() public step1description: string;

  @Input() public races: Race[] = [];

  constructor() {}

  ngOnInit() {}

  showSelectedRace(val: any) {
    if (val == "All Races") {
      for (let race of this.races) {
        race.show = "true";
      }
    } else {
      for (let race of this.races) {
        if (val != race.positionName) {
          race.show = "false";
        } else {
          race.show = "true";
        }
      }
    }
  }
}
