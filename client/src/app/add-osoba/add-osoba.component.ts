import { Component, Injector } from '@angular/core';
import { AddOsobaGenerated } from './add-osoba-generated.component';

@Component({
  selector: 'page-add-osoba',
  templateUrl: './add-osoba.component.html'
})
export class AddOsobaComponent extends AddOsobaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
