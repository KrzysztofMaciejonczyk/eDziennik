/*
  This file is automatically generated. Any changes will be overwritten.
  Modify nauczyciel.component.ts instead.
*/
import { LOCALE_ID, ChangeDetectorRef, ViewChild, AfterViewInit, Injector, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Subscription } from 'rxjs';

import { DialogService, DIALOG_PARAMETERS, DialogRef } from '@radzen/angular/dist/dialog';
import { NotificationService } from '@radzen/angular/dist/notification';
import { ContentComponent } from '@radzen/angular/dist/content';
import { HeadingComponent } from '@radzen/angular/dist/heading';
import { GridComponent } from '@radzen/angular/dist/grid';

import { ConfigService } from '../config.service';
import { AddNauczycielComponent } from '../add-nauczyciel/add-nauczyciel.component';
import { EditNauczycielComponent } from '../edit-nauczyciel/edit-nauczyciel.component';

import { BazaService } from '../baza.service';
import { SecurityService } from '../security.service';

export class NauczycielGenerated implements AfterViewInit, OnInit, OnDestroy {
  // Components
  @ViewChild('content1') content1: ContentComponent;
  @ViewChild('pageTitle') pageTitle: HeadingComponent;
  @ViewChild('grid0') grid0: GridComponent;

  router: Router;

  cd: ChangeDetectorRef;

  route: ActivatedRoute;

  notificationService: NotificationService;

  configService: ConfigService;

  dialogService: DialogService;

  dialogRef: DialogRef;

  httpClient: HttpClient;

  locale: string;

  _location: Location;

  _subscription: Subscription;

  baza: BazaService;

  security: SecurityService;
  parameters: any;
  getNauczycielsResult: any;
  getNauczycielsCount: any;

  constructor(private injector: Injector) {
  }

  ngOnInit() {
    this.notificationService = this.injector.get(NotificationService);

    this.configService = this.injector.get(ConfigService);

    this.dialogService = this.injector.get(DialogService);

    this.dialogRef = this.injector.get(DialogRef, null);

    this.locale = this.injector.get(LOCALE_ID);

    this.router = this.injector.get(Router);

    this.cd = this.injector.get(ChangeDetectorRef);

    this._location = this.injector.get(Location);

    this.route = this.injector.get(ActivatedRoute);

    this.httpClient = this.injector.get(HttpClient);

    this.baza = this.injector.get(BazaService);
    this.security = this.injector.get(SecurityService);
  }

  ngAfterViewInit() {
    this._subscription = this.route.params.subscribe(parameters => {
      if (this.dialogRef) {
        this.parameters = this.injector.get(DIALOG_PARAMETERS);
      } else {
        this.parameters = parameters;
      }
      this.load();
      this.cd.detectChanges();
    });
  }

  ngOnDestroy() {
    if (this._subscription) {
      this._subscription.unsubscribe();
    }
  }


  load() {
    this.grid0.load();
  }

  grid0Add(event: any) {
    this.dialogService.open(AddNauczycielComponent, { parameters: {}, title: 'Add Nauczyciel' });
  }

  grid0Delete(event: any) {
    this.baza.deleteNauczyciel(event.nauczyciel_id)
    .subscribe((result: any) => {
      this.notificationService.notify({ severity: "success", summary: `Success`, detail: `Nauczyciel deleted!` });
    }, (result: any) => {
      this.notificationService.notify({ severity: "error", summary: `Error`, detail: `Unable to delete Nauczyciel` });
    });
  }

  grid0LoadData(event: any) {
    this.baza.getNauczyciels(`${event.filter}`, event.top, event.skip, `${event.orderby}`, event.top != null && event.skip != null, `Klasas,Przedmiots,Uwagas,Ocenas,Osoba`, null, null)
    .subscribe((result: any) => {
      this.getNauczycielsResult = result.value;

      this.getNauczycielsCount = event.top != null && event.skip != null ? result['@odata.count'] : result.value.length;
    }, (result: any) => {
      this.notificationService.notify({ severity: "error", summary: `Error`, detail: `Unable to load Nauczyciels` });
    });
  }

  grid0RowSelect(event: any) {
    this.dialogService.open(EditNauczycielComponent, { parameters: {nauczyciel_id: event.nauczyciel_id}, title: 'Edit Nauczyciel' });
  }
}
