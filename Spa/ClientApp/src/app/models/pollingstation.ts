export class PollingStation {
  pollingStationId  : number;
  name              : string;
  additionalInfo    : string;
  latitude          : number;
  longitute         : number; //TEMPORARILY MISSPELLED TO MATCH API, FIX WHEN API IS FIXED
  electionId        : number;
  address           : string;
  wheelchairInfo    : string;
  parkingInfo       : string;
  washroomInfo      : string;
  generalAccessInfo : string;


  constructor(
    pollingStationId: number,
    name: string,
    additionalInfo: string,
    latitude: number,
    longitute: number, //TEMPORARILY MISSPELLED TO MATCH API, FIX WHEN API IS FIXED
    electionId: number,
    address: string,
    wheelchairInfo: string,
    parkingInfo: string,
    washroomInfo: string,
    generalAccessInfo: string,
  ) {
    this.pollingStationId   = pollingStationId;
    this.name               = name;
    this.additionalInfo     = additionalInfo;
    this.latitude           = latitude;
    this.longitute          = longitute; //TEMPORARILY MISSPELLED TO MATCH API, FIX WHEN API IS FIXED
    this.electionId         = electionId;
    this.address            = address;
    this.wheelchairInfo     = wheelchairInfo;
    this.parkingInfo        = parkingInfo;
    this.washroomInfo       = washroomInfo;
    this.generalAccessInfo = generalAccessInfo;
  }
}
