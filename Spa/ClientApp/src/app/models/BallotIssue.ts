import { IssueOption } from './IssueOption';

export class BallotIssue {
  ballotIssueId : number;
  electionId: number;
  election: string;
  ballotIssueTitle : string;
  description: string;
  ballotIssueOptions: IssueOption[];
  answer: string;

  constructor(
    ballotIssueId: number,
    electionId: number,
    election: string,
    ballotIssueTitle: string,
    description: string,
    ballotIssueOptions: IssueOption[],
    answer: string
  ) {
    this.ballotIssueId = ballotIssueId;
    this.electionId = electionId;
    this.election = election;
    this.ballotIssueTitle = ballotIssueTitle;
    this.ballotIssueOptions = ballotIssueOptions;
    this.description = description;
    this.answer = answer;
  }
}
