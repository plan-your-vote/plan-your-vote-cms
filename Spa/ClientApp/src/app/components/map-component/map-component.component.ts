import { Component, OnInit } from '@angular/core';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import { fromLonLat } from 'ol/proj';
import { PollingStation } from '../../models/pollingstation';
import { PollingStationService } from '../../services/pollingstation.service';

@Component({
  selector: 'app-map-component',
  templateUrl: './map-component.component.html',
  styleUrls: ['./map-component.component.less']
})
export class MapComponentComponent implements OnInit {
  public stations: PollingStation[] = [];

  constructor(private pollingStationApi: PollingStationService) {}
  getStations(): void {
    this.pollingStationApi.getPollingStations().subscribe(data => (this.stations = data));
  };

  ngOnInit() {
    this.getStations();
    //Creates the map
    const map = new Map({
      layers: [
        new TileLayer({
          source: new OSM()
        })
      ],
      target: 'map'
    });
    //Centers map on current location
    navigator.geolocation.getCurrentPosition(function (pos) {
      const coords = fromLonLat([pos.coords.longitude, pos.coords.latitude]);
      map.getView().animate({ center: coords, zoom: 10 });
    });
    alert(this.stations[0]);
  }
}
