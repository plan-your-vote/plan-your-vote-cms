export class IssueOption{
  issueOptionId: number;
  issueOptionTitle: string;
  issueOptionInfo: string;
  ballotIssueId: number;

  constructor(
    issueOptionId: number,
    issueOptionTitle: string,
    issueOptionInfo: string,
    ballotIssueId: number
  ) {
    this.issueOptionId = issueOptionId;
    this.issueOptionTitle = issueOptionTitle;
    this.issueOptionInfo = issueOptionInfo;
    this.ballotIssueId = ballotIssueId
  }
}
