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
import { disableBindings } from '@angular/core/src/render3';

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
          navigator.geolocation.getCurrentPosition(function (pos) {
            var lat1 = pos.coords.latitude;
            var lat2 = station.latitude;
            var lon1 = pos.coords.longitude;
            var lon2 = station.longitute;

            var radlat1 = Math.PI * lat1 / 180;
            var radlat2 = Math.PI * lat2 / 180;
            var theta = lon1 - lon2;
            var radtheta = Math.PI * theta / 180;
            var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
            if (dist > 1) {
              dist = 1;
            }
            dist = Math.acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344;
            if (dist < 5) {
              console.log("Distance to polling station is: " + dist + "km. adding feature to map.")
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
            } else {
              console.log("Distance to polling station is: " + dist + "km. skipping station.")
            }

          });
        })
      });
  }
}
