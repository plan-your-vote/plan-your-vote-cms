import { Candidate } from './candidate';

export class CandidateRace {
  candidateRaceId: number;
  positionName: string;
  platformInfo: string;
  topIssues: string;
  candidateId: number;
  raceId: number;
  candidate: Candidate
  constructor(
    candidateRaceId: number,
    positionName: string,
    platformInfo: string,
    topIssues: string,
    candidateId: number,
    raceId: number,
    candidate: Candidate
  ) {
    this.candidateRaceId = candidateRaceId;
    this.candidateId = candidateId;
    this.positionName = positionName;
    this.platformInfo = platformInfo;
    this.topIssues = topIssues;
    this.raceId = raceId;
    this.candidate = candidate;
  }
}
