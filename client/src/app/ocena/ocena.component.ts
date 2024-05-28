import { Component, Injector } from '@angular/core';
import { OcenaGenerated } from './ocena-generated.component';

@Component({
  selector: 'page-ocena',
  templateUrl: './ocena.component.html'
})
export class OcenaComponent extends OcenaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
