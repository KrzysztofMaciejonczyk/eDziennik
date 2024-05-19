import { Component, Injector } from '@angular/core';
import { AddKlasaGenerated } from './add-klasa-generated.component';

@Component({
  selector: 'page-add-klasa',
  templateUrl: './add-klasa.component.html'
})
export class AddKlasaComponent extends AddKlasaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
