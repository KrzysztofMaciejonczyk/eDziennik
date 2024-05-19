import { Component, Injector } from '@angular/core';
import { EditOsobaGenerated } from './edit-osoba-generated.component';

@Component({
  selector: 'page-edit-osoba',
  templateUrl: './edit-osoba.component.html'
})
export class EditOsobaComponent extends EditOsobaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
