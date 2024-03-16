import { NuclearResponse } from './../../../models/nuclearResponse.model';
import {
  AfterContentInit,
  AfterViewInit,
  Component,
  OnInit,
} from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { latLng, Map, marker, tileLayer, geoJSON } from 'leaflet';
import { NuclearService } from 'src/app/services/nuclear.service';
import { timeInterval } from 'rxjs';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-radiation-map',
  templateUrl: './radiation-map.component.html',
  styleUrls: ['./radiation-map.component.css'],
})
export class RadiationMapComponent implements AfterViewInit, AfterContentInit {
  geojsonData: any;
  map!: Map;
  safeHtml!: SafeHtml;

  constructor(
    private http: HttpClient,
    private nuclearService: NuclearService,
    private sanitizer: DomSanitizer
  ) {}
  ngAfterContentInit(): void {}

  ngAfterViewInit(): void {}

  ngDoCheck(): void {
    if (!this.map) {
      this.map = new Map('map', {
        scrollWheelZoom: false, // Disable scroll zoom
        dragging: false, // Disable dragging
        doubleClickZoom: false,
      }).setView([48.21, 31.1], 6);
      tileLayer(
        'https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}',
        {
          attribution:
            'Tiles &copy; Esri &mdash; Esri, DeLorme, NAVTEQ, TomTom, Intermap, iPC, USGS, FAO, NPS, NRCAN, GeoBase, Kadaster NL, Ordnance Survey, Esri Japan, METI, Esri China (Hong Kong), and the GIS User Community',
        }
      ).addTo(this.map);
      const geoJsonFileNames = [
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
      const geoJsonsFiles: any[] = [];

      geoJsonFileNames.forEach((fileName) => {
        geoJsonsFiles.push(this.readAndAddGeoJson(fileName));
      });
    }
  }

  private readAndAddGeoJson(fileName: string): any {
    this.http
      .get(`./assets/geojsons/ukraine_geojson-master/${fileName}`)
      .subscribe((data) => {
        let geojsonData: any = data;

        // Add GeoJSON to the map after fetching data

        this.nuclearService
          .getByDistrict(geojsonData.properties['name:uk'])
          .subscribe((x) => {
            geojsonData.properties.radiatinValue = (<NuclearResponse>(
              x.resultObj
            ))?.value!;
            console.log(x.resultObj?.value);
            console.log(geojsonData); // Log the GeoJSON data to the console
            this.addPopUpData(geojsonData);
            geoJSON(geojsonData, {
              style: (feature: any) => {
                return this.addStyleToGEOJSON(
                  geojsonData.properties.radiatinValue
                );
              },
              onEachFeature: (feature: any, layer: any) =>
                this.onEachFeature(feature, layer),
            }).addTo(this.map);
          });
        return geojsonData;
      });
  }

  private addPopUpData(geojsonData: any) {
    const popupContent =
      '<div class="flex flex-col space-y-3 p-1 justify-center items-center"><span class=" text-lg text-blue-300">' +
      geojsonData.properties.name +
      '</span><span class=" text-md text-green-600">Radiation:' +
      geojsonData.properties.radiatinValue +
      ' µSv/h</span>' +
      '<div>' +
      `<a  href="/maps/radiation-map/${geojsonData.properties.radiatinValue}" class="px-4 py-1 border border-green-400 hover:border-green-700 rounded-md  hover:text-green-700 duration-200">☢️ More</a>`;
    +'</div>';
    geojsonData.properties.popupContent = popupContent;
    +'</div>';
  }

  private onEachFeature(feature: any, layer: any) {
    if (feature.properties && feature.properties.popupContent) {
      layer.bindPopup(feature.properties.popupContent);
    }
  }
  private addStyleToGEOJSON(radiationValue: number): { color: string } {
    if (radiationValue < 0.3) {
      return {
        color: '	#44ce1b',
      };
    }
    if (radiationValue >= 0.3 && +radiationValue <= 1) {
      return {
        color: '	#bbdb44',
      };
    }
    if (+radiationValue > 1 && +radiationValue < 5) {
      return {
        color: '	#f7e379',
      };
    }
    if (+radiationValue >= 5 && +radiationValue <= 10) {
      return {
        color: '	#f2a134',
      };
    }
    if (+radiationValue > 10) {
      return {
        color: '	#e51f1f',
      };
    }
    return {
      color: '	#00ff00',
    };
  }
}
