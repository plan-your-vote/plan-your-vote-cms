import { Component, OnInit, Input } from "@angular/core";
import { BallotIssue } from "src/app/models/BallotIssue";

@Component({
  selector: "app-step2",
  templateUrl: "./step2.component.html",
  styleUrls: ["./step2.component.less"],

  inputs: ["step2title", "step2description", "issues"]
})
export class Step2Component implements OnInit {
  @Input() public step2title: string;
  @Input() public step2description: string;

  @Input() public issues: BallotIssue[] = [];
  
  constructor() {}

  ngOnInit() {}
}
