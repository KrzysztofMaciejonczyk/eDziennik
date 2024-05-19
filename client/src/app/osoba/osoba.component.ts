import { Component, Injector } from '@angular/core';
import { OsobaGenerated } from './osoba-generated.component';

@Component({
  selector: 'page-osoba',
  templateUrl: './osoba.component.html'
})
export class OsobaComponent extends OsobaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
