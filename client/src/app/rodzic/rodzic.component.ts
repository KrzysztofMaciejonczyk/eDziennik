import { Component, Injector } from '@angular/core';
import { RodzicGenerated } from './rodzic-generated.component';

@Component({
  selector: 'page-rodzic',
  templateUrl: './rodzic.component.html'
})
export class RodzicComponent extends RodzicGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
