import { Component, Injector } from '@angular/core';
import { DodajNauczycielaGenerated } from './dodaj-nauczyciela-generated.component';

@Component({
  selector: 'page-dodaj-nauczyciela',
  templateUrl: './dodaj-nauczyciela.component.html'
})
export class DodajNauczycielaComponent extends DodajNauczycielaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
