import { Component, Injector } from '@angular/core';
import { ObecnoscGenerated } from './obecnosc-generated.component';

@Component({
  selector: 'page-obecnosc',
  templateUrl: './obecnosc.component.html'
})
export class ObecnoscComponent extends ObecnoscGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
