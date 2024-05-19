import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';

import { ConfigService } from './config.service';
import { ODataClient } from './odata-client';
import * as models from './baza.model';

@Injectable()
export class BazaService {
  odata: ODataClient;
  basePath: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.basePath = config.get('baza');
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  getDataOpis(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/DataOpis`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createDataOpi(expand: string | null, dataOpi: models.DataOpi | null) : Observable<any> {
    return this.odata.post(`/DataOpis`, dataOpi, { expand }, []);
  }

  deleteDataOpi(dataOpisId: number | null) : Observable<any> {
    return this.odata.delete(`/DataOpis(${dataOpisId})`, item => !(item.data_opis_id == dataOpisId));
  }

  getDataOpiBydataOpisId(expand: string | null, dataOpisId: number | null) : Observable<any> {
    return this.odata.getById(`/DataOpis(${dataOpisId})`, { expand });
  }

  updateDataOpi(expand: string | null, dataOpisId: number | null, dataOpi: models.DataOpi | null) : Observable<any> {
    return this.odata.patch(`/DataOpis(${dataOpisId})`, dataOpi, item => item.data_opis_id == dataOpisId, { expand }, []);
  }

  getKlasas(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Klasas`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createKlasa(expand: string | null, klasa: models.Klasa | null) : Observable<any> {
    return this.odata.post(`/Klasas`, klasa, { expand }, ['Nauczyciel']);
  }

  deleteKlasa(klasaId: number | null) : Observable<any> {
    return this.odata.delete(`/Klasas(${klasaId})`, item => !(item.klasa_id == klasaId));
  }

  getKlasaByklasaId(expand: string | null, klasaId: number | null) : Observable<any> {
    return this.odata.getById(`/Klasas(${klasaId})`, { expand });
  }

  updateKlasa(expand: string | null, klasaId: number | null, klasa: models.Klasa | null) : Observable<any> {
    return this.odata.patch(`/Klasas(${klasaId})`, klasa, item => item.klasa_id == klasaId, { expand }, ['Nauczyciel']);
  }

  getNauczyciels(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Nauczyciels`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createNauczyciel(expand: string | null, nauczyciel: models.Nauczyciel | null) : Observable<any> {
    return this.odata.post(`/Nauczyciels`, nauczyciel, { expand }, ['Osoba']);
  }

  deleteNauczyciel(nauczycielId: number | null) : Observable<any> {
    return this.odata.delete(`/Nauczyciels(${nauczycielId})`, item => !(item.nauczyciel_id == nauczycielId));
  }

  getNauczycielBynauczycielId(expand: string | null, nauczycielId: number | null) : Observable<any> {
    return this.odata.getById(`/Nauczyciels(${nauczycielId})`, { expand });
  }

  updateNauczyciel(expand: string | null, nauczycielId: number | null, nauczyciel: models.Nauczyciel | null) : Observable<any> {
    return this.odata.patch(`/Nauczyciels(${nauczycielId})`, nauczyciel, item => item.nauczyciel_id == nauczycielId, { expand }, ['Osoba']);
  }

  getObecnoscs(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Obecnoscs`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createObecnosc(expand: string | null, obecnosc: models.Obecnosc | null) : Observable<any> {
    return this.odata.post(`/Obecnoscs`, obecnosc, { expand }, ['Uczen', 'DataOpi']);
  }

  deleteObecnosc(ocenaId: number | null) : Observable<any> {
    return this.odata.delete(`/Obecnoscs(${ocenaId})`, item => !(item.ocena_id == ocenaId));
  }

  getObecnoscByocenaId(expand: string | null, ocenaId: number | null) : Observable<any> {
    return this.odata.getById(`/Obecnoscs(${ocenaId})`, { expand });
  }

  updateObecnosc(expand: string | null, ocenaId: number | null, obecnosc: models.Obecnosc | null) : Observable<any> {
    return this.odata.patch(`/Obecnoscs(${ocenaId})`, obecnosc, item => item.ocena_id == ocenaId, { expand }, ['Uczen','DataOpi']);
  }

  getOcenas(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Ocenas`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOcena(expand: string | null, ocena: models.Ocena | null) : Observable<any> {
    return this.odata.post(`/Ocenas`, ocena, { expand }, ['Nauczyciel', 'Uczen', 'DataOpi']);
  }

  deleteOcena(ocenaId: number | null) : Observable<any> {
    return this.odata.delete(`/Ocenas(${ocenaId})`, item => !(item.ocena_id == ocenaId));
  }

  getOcenaByocenaId(expand: string | null, ocenaId: number | null) : Observable<any> {
    return this.odata.getById(`/Ocenas(${ocenaId})`, { expand });
  }

  updateOcena(expand: string | null, ocenaId: number | null, ocena: models.Ocena | null) : Observable<any> {
    return this.odata.patch(`/Ocenas(${ocenaId})`, ocena, item => item.ocena_id == ocenaId, { expand }, ['Nauczyciel','Uczen','DataOpi']);
  }

  getOsobas(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Osobas`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOsoba(expand: string | null, osoba: models.Osoba | null) : Observable<any> {
    return this.odata.post(`/Osobas`, osoba, { expand }, []);
  }

  deleteOsoba(osobaId: number | null) : Observable<any> {
    return this.odata.delete(`/Osobas(${osobaId})`, item => !(item.osoba_id == osobaId));
  }

  getOsobaByosobaId(expand: string | null, osobaId: number | null) : Observable<any> {
    return this.odata.getById(`/Osobas(${osobaId})`, { expand });
  }

  updateOsoba(expand: string | null, osobaId: number | null, osoba: models.Osoba | null) : Observable<any> {
    return this.odata.patch(`/Osobas(${osobaId})`, osoba, item => item.osoba_id == osobaId, { expand }, []);
  }

  getPrzedmiots(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Przedmiots`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createPrzedmiot(expand: string | null, przedmiot: models.Przedmiot | null) : Observable<any> {
    return this.odata.post(`/Przedmiots`, przedmiot, { expand }, ['Nauczyciel', 'Klasa']);
  }

  deletePrzedmiot(przedmiotId: number | null) : Observable<any> {
    return this.odata.delete(`/Przedmiots(${przedmiotId})`, item => !(item.przedmiot_id == przedmiotId));
  }

  getPrzedmiotByprzedmiotId(expand: string | null, przedmiotId: number | null) : Observable<any> {
    return this.odata.getById(`/Przedmiots(${przedmiotId})`, { expand });
  }

  updatePrzedmiot(expand: string | null, przedmiotId: number | null, przedmiot: models.Przedmiot | null) : Observable<any> {
    return this.odata.patch(`/Przedmiots(${przedmiotId})`, przedmiot, item => item.przedmiot_id == przedmiotId, { expand }, ['Nauczyciel','Klasa']);
  }

  getRodzics(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Rodzics`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createRodzic(expand: string | null, rodzic: models.Rodzic | null) : Observable<any> {
    return this.odata.post(`/Rodzics`, rodzic, { expand }, ['Osoba']);
  }

  deleteRodzic(rodzicId: number | null) : Observable<any> {
    return this.odata.delete(`/Rodzics(${rodzicId})`, item => !(item.rodzic_id == rodzicId));
  }

  getRodzicByrodzicId(expand: string | null, rodzicId: number | null) : Observable<any> {
    return this.odata.getById(`/Rodzics(${rodzicId})`, { expand });
  }

  updateRodzic(expand: string | null, rodzicId: number | null, rodzic: models.Rodzic | null) : Observable<any> {
    return this.odata.patch(`/Rodzics(${rodzicId})`, rodzic, item => item.rodzic_id == rodzicId, { expand }, ['Osoba']);
  }

  getUczens(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Uczens`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createUczen(expand: string | null, uczen: models.Uczen | null) : Observable<any> {
    return this.odata.post(`/Uczens`, uczen, { expand }, ['Osoba', 'Rodzic', 'Klasa']);
  }

  deleteUczen(uczenId: number | null) : Observable<any> {
    return this.odata.delete(`/Uczens(${uczenId})`, item => !(item.uczen_id == uczenId));
  }

  getUczenByuczenId(expand: string | null, uczenId: number | null) : Observable<any> {
    return this.odata.getById(`/Uczens(${uczenId})`, { expand });
  }

  updateUczen(expand: string | null, uczenId: number | null, uczen: models.Uczen | null) : Observable<any> {
    return this.odata.patch(`/Uczens(${uczenId})`, uczen, item => item.uczen_id == uczenId, { expand }, ['Osoba','Rodzic','Klasa']);
  }

  getUwagas(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Uwagas`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createUwaga(expand: string | null, uwaga: models.Uwaga | null) : Observable<any> {
    return this.odata.post(`/Uwagas`, uwaga, { expand }, ['Nauczyciel', 'Uczen', 'DataOpi']);
  }

  deleteUwaga(uwagaId: number | null) : Observable<any> {
    return this.odata.delete(`/Uwagas(${uwagaId})`, item => !(item.uwaga_id == uwagaId));
  }

  getUwagaByuwagaId(expand: string | null, uwagaId: number | null) : Observable<any> {
    return this.odata.getById(`/Uwagas(${uwagaId})`, { expand });
  }

  updateUwaga(expand: string | null, uwagaId: number | null, uwaga: models.Uwaga | null) : Observable<any> {
    return this.odata.patch(`/Uwagas(${uwagaId})`, uwaga, item => item.uwaga_id == uwagaId, { expand }, ['Nauczyciel','Uczen','DataOpi']);
  }
}
