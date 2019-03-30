export class PollingStation {
  pollingStationId  : number;
  name              : string;
  additionalInfo    : string;
  latitude          : number;
  longitude         : number;
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
    longitude: number,
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
    this.longitude          = longitude;
    this.electionId         = electionId;
    this.address            = address;
    this.wheelchairInfo     = wheelchairInfo;
    this.parkingInfo        = parkingInfo;
    this.washroomInfo       = washroomInfo;
    this.generalAccessInfo = generalAccessInfo;
  }
}
