export class Candidate {
  candidateId: number;
  firstName: string;
  lastName: string;
  picture: string;
  organization: string;

  constructor(
    candidateId: number,
    firstName: string,
    lastName: string,
    picture: string,
    organization: string
  ) {
    this.candidateId = candidateId;
    this.firstName = firstName;
    this.lastName = lastName;
    this.picture = picture;
    this.organization = organization;
  }
}
