import { Component, OnInit } from '@angular/core';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import Feature from 'ol/Feature';
import Point from 'ol/geom/Point';
import sourceVector from 'ol/source/Vector';
import layerVector from 'ol/layer/Vector';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import { transform } from 'ol/proj';
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

  ngOnInit() {
    const map = new Map({
      layers: [
        new TileLayer({
          source: new OSM()
        })
      ],
      target: 'map'
    });

    // Centers map on current location
    navigator.geolocation.getCurrentPosition(function (pos) {
      const coords = fromLonLat([pos.coords.longitude, pos.coords.latitude]);
      map.getView().animate({ center: coords, zoom: 10 });
    });

    this.pollingStationApi
      .getPollingStations()
      .then(pollingStations => {
        this.stations = pollingStations;
        console.table(this.stations);
      })
      .then(() => {
        this.stations.forEach(function (station) {
          console.log(station.longitute);

          var marker = new Feature({
            geometry: new Point(transform([station.longitute, station.latitude], 'EPSG:4326', 'EPSG:3857')),
          });

          var markers = new sourceVector({
            features: [marker]
          });

          var markerVectorLayer = new layerVector({
            source: markers,
          });
          map.addLayer(markerVectorLayer);
        })
      });
  }
}
