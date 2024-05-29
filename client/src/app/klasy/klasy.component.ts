import { Component, Injector } from '@angular/core';
import { KlasyGenerated } from './klasy-generated.component';

@Component({
  selector: 'page-klasy',
  templateUrl: './klasy.component.html'
})
export class KlasyComponent extends KlasyGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
