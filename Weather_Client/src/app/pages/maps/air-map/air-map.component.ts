import { AirService } from './../../../services/air.service';
import { Component } from '@angular/core';
import { latLng, Map, marker, tileLayer, geoJSON, popup } from 'leaflet';
import { airPollutionData } from 'src/app/models/air/airPollutionData.model';
interface City {
  name: string;
  latitude: number;
  longitude: number;
}
@Component({
  selector: 'app-air-map',
  templateUrl: './air-map.component.html',
  styleUrls: ['./air-map.component.css'],
})
export class AirMapComponent {
  map!: Map;

  cities: City[] = [
    { name: 'Київ', latitude: 50.45, longitude: 30.5233 },
    { name: 'Одеса', latitude: 46.4775, longitude: 30.7326 },
    { name: 'Дніпро', latitude: 48.4675, longitude: 35.04 },
    { name: 'Донецьк', latitude: 48.0028, longitude: 37.8053 },
    { name: 'Запоріжжя', latitude: 47.85, longitude: 35.1175 },
    { name: 'Львів', latitude: 49.8425, longitude: 24.0322 },
    { name: 'Кривий Ріг', latitude: 47.9086, longitude: 33.3433 },
    { name: 'Севастополь', latitude: 44.605, longitude: 33.5225 },
    { name: 'Миколаїв', latitude: 46.975, longitude: 31.995 },
    { name: 'Маріуполь', latitude: 47.0958, longitude: 37.5494 },
    { name: 'Вінниця', latitude: 49.2333, longitude: 28.4833 },
    { name: 'Макіївка', latitude: 48.0556, longitude: 37.9611 },
    { name: 'Сімферополь', latitude: 44.9484, longitude: 34.1 },
    { name: 'Полтава', latitude: 49.5894, longitude: 34.5514 },
    { name: 'Чернігів', latitude: 51.4939, longitude: 31.2947 },
    { name: 'Херсон', latitude: 46.6425, longitude: 32.625 },
    { name: 'Черкаси', latitude: 49.4444, longitude: 32.0597 },
    { name: 'Хмельницький', latitude: 49.4167, longitude: 27.0 },
    { name: 'Чернівці', latitude: 48.3, longitude: 25.9333 },
    { name: 'Суми', latitude: 50.9167, longitude: 34.75 },
  ];

  constructor(private airService: AirService) {}

  ngDoCheck(): void {
    if (!this.map) {
      this.map = new Map('map', {
        scrollWheelZoom: false, // Disable scroll zoom
        dragging: false, // Disable dragging
        doubleClickZoom: false,
      }).setView([48.21, 31.1], 6);

      this.cities.forEach((city) => {
        tileLayer(
          'https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}',
          {
            attribution:
              'Tiles &copy; Esri &mdash; Esri, DeLorme, NAVTEQ, TomTom, Intermap, iPC, USGS, FAO, NPS, NRCAN, GeoBase, Kadaster NL, Ordnance Survey, Esri Japan, METI, Esri China (Hong Kong), and the GIS User Community',
          }
        ).addTo(this.map);
        let airPollutionDataProp: airPollutionData;
        this.airService
          .getByCity(city.latitude, city.longitude)
          .subscribe((x) => {
            airPollutionDataProp = <airPollutionData>x.resultObj;
            let popupContent =
              '<div class="flex flex-col space-y-3 p-1 justify-center items-center"><span class=" text-lg text-blue-300">' +
              city.name +
              '</span><span class=" text-md text-green-600">Pm2_5:' +
              airPollutionDataProp.list[0].components.pm2_5 +
              ' µSv/h</span>' +
              '<div>' +
              `<a  href="/maps/air-map/${airPollutionDataProp.list[0].components.pm2_5}" class="px-4 py-1 border border-green-400 hover:border-green-700 rounded-md  hover:text-green-700 duration-200">🍃 More</a>`;
            +'</div>';
            ' geojsonData.properties.popupContent = popupContent;' + '</div>';
            marker({ lat: city.latitude, lng: city.longitude })
              .bindPopup(popup().setContent(popupContent))
              .addTo(this.map);
          });
      });
    }
  }
}
