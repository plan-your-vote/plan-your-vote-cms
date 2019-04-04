import { Component, OnInit } from '@angular/core';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import { fromLonLat } from 'ol/proj';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.less']
})
export class MapComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    const cambieAnd12thLonLat = [-123.115083, 49.260472];
    const cambieAnd12thMercator = fromLonLat(cambieAnd12thLonLat);

    const map = new Map({
      layers: [
        new TileLayer({
          source: new OSM()
        })
      ],
      target: 'map',
      view: new View({
        center: cambieAnd12thMercator,
        zoom: 12
      })
    });
  }

}
