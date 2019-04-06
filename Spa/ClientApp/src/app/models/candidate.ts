export class Candidate {
  candidateId : number;
  firstName   : string;
  lastName    : string;
  picture     : string;
  organization: string;
  selected: boolean;

  constructor(
    candidateId : number,
    firstName   : string,
    lastName    : string,
    picture     : string,
    organization: string,
    selected    : boolean
  ) {
    this.candidateId  = candidateId;
    this.firstName    = firstName;
    this.lastName     = lastName;
    this.picture      = picture;
    this.organization = organization;
    this.selected     = false;
  }
}
