/*
  This file is automatically generated. Any changes will be overwritten.
  Modify add-osoba.component.ts instead.
*/
import { LOCALE_ID, ChangeDetectorRef, ViewChild, AfterViewInit, Injector, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Subscription } from 'rxjs';

import { DialogService, DIALOG_PARAMETERS, DialogRef } from '@radzen/angular/dist/dialog';
import { NotificationService } from '@radzen/angular/dist/notification';
import { ContentComponent } from '@radzen/angular/dist/content';
import { TemplateFormComponent } from '@radzen/angular/dist/template-form';
import { LabelComponent } from '@radzen/angular/dist/label';
import { TextBoxComponent } from '@radzen/angular/dist/textbox';
import { ButtonComponent } from '@radzen/angular/dist/button';

import { ConfigService } from '../config.service';

import { BazaService } from '../baza.service';
import { SecurityService } from '../security.service';

export class AddOsobaGenerated implements AfterViewInit, OnInit, OnDestroy {
  // Components
  @ViewChild('content1') content1: ContentComponent;
  @ViewChild('form0') form0: TemplateFormComponent;
  @ViewChild('imieLabel') imieLabel: LabelComponent;
  @ViewChild('imie') imie: TextBoxComponent;
  @ViewChild('emailLabel') emailLabel: LabelComponent;
  @ViewChild('email') email: TextBoxComponent;
  @ViewChild('nazwiskoLabel') nazwiskoLabel: LabelComponent;
  @ViewChild('nazwisko') nazwisko: TextBoxComponent;
  @ViewChild('telefonLabel') telefonLabel: LabelComponent;
  @ViewChild('telefon') telefon: TextBoxComponent;
  @ViewChild('hasloLabel') hasloLabel: LabelComponent;
  @ViewChild('haslo') haslo: TextBoxComponent;
  @ViewChild('button1') button1: ButtonComponent;
  @ViewChild('button2') button2: ButtonComponent;

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

  }

  form0Submit(event: any) {
    this.baza.createOsoba(null, event)
    .subscribe((result: any) => {
      if (this.dialogRef) {
        this.dialogRef.close();
      } else {
        this._location.back();
      }
    }, (result: any) => {
      this.notificationService.notify({ severity: "error", summary: `Error`, detail: `Unable to create new Osoba!` });
    });
  }
}
