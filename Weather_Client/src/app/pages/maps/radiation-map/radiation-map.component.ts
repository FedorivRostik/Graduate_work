import { AfterViewInit, Component, OnInit } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { latLng, Map, marker, tileLayer, geoJSON } from 'leaflet';

@Component({
  selector: 'app-radiation-map',
  templateUrl: './radiation-map.component.html',
  styleUrls: ['./radiation-map.component.css'],
})
export class RadiationMapComponent implements AfterViewInit, OnInit {
  geojsonData: any;
  map!: Map;
  constructor(private http: HttpClient) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    this.map = new Map('map').setView([48.21, 31.1], 6);
    tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution:
        '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(this.map);
    const geoJsonFiles = [
      'UA_05_Vinnytska.geojson',
      'UA_07_Volynska.geojson',
      'UA_09_Luhanska.geojson',
      'UA_12_Dnipropetrovska.geojson',
      'UA_14_Donetska.geojson',
      'UA_18_Zhytomyrska.geojson',
      'UA_21_Zakarpatska.geojson',
      'UA_23_Zaporizka.geojson',
      'UA_26_Ivano_Frankivska.geojson',
      'UA_32_Kyivska.geojson',
      'UA_35_Kirovohradska.geojson',
      'UA_43_Avtonomna_Respublika_Krym.geojson',
      'UA_46_Lvivska.geojson',
      'UA_48_Mykolaivska.geojson',
      'UA_51_Odeska.geojson',
      'UA_53_Poltavska.geojson',
      'UA_56_Rivnenska.geojson',
      'UA_59_Sumska.geojson',
      'UA_61_Ternopilska.geojson',
      'UA_63_Kharkivska.geojson',
      'UA_65_Khersonska.geojson',
      'UA_68_Khmelnytska.geojson',
      'UA_71_Cherkaska.geojson',
      'UA_74_Chernihivska.geojson',
      'UA_77_Chernivetska.geojson',
    ];

    geoJsonFiles.forEach((fileName) => {
      this.readAndAddGeoJson(fileName);
    });
  }

  private readAndAddGeoJson(fileName: string) {
    this.http
      .get(`./assets/geojsons/ukraine_geojson-master/${fileName}`)
      .subscribe((data) => {
        this.geojsonData = data;
        console.log(this.geojsonData); // Log the GeoJSON data to the console
        // Add GeoJSON to the map after fetching data
        this.addPopUpData(this.geojsonData);

        geoJSON(this.geojsonData, {
          style: (feature: any) => {
            return this.addStyleToGEOJSON(feature);
          },
          onEachFeature: (feature: any, layer: any) =>
            this.onEachFeature(feature, layer),
        }).addTo(this.map);
      });
  }
  private addPopUpData(geojsonData: any) {
    geojsonData.properties.popupContent = geojsonData.properties.name;
  }
  private onEachFeature(feature: any, layer: any) {
    if (feature.properties && feature.properties.popupContent) {
      layer.bindPopup(feature.properties.popupContent);
    }
  }
  private addStyleToGEOJSON(feature: any): { color: string } {
    if (feature.properties.isOcupated) {
      return {
        color: '#ff0000',
      };
    }
    return {
      color: '	#00ff00',
    };
  }
}
