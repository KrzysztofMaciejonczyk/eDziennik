import { Component, Injector } from '@angular/core';
import { KlasaGenerated } from './klasa-generated.component';

@Component({
  selector: 'page-klasa',
  templateUrl: './klasa.component.html'
})
export class KlasaComponent extends KlasaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
