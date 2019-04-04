import { CandidateRace } from './CandidateRace';
import { Candidate } from './candidate';

export class Race {
  raceId : number;
  electionId: number;
  election: string;
  positionName : string;
  numberNeeded: number;
  candidateRaces: CandidateRace[];
  show: string;
  selected: Candidate[];

  constructor(
    raceId: number,
    electionId: number,
    election: string,
    positionName: string,
    numberNeeded: number,
    candidateRaces: CandidateRace[],
    show: string
  ) {
    this.raceId = raceId;
    this.electionId = electionId;
    this.election = election;
    this.positionName = positionName;
    this.numberNeeded = numberNeeded;
    this.candidateRaces = candidateRaces;
    this.show = "true";
    this.selected = [];

  }
}
