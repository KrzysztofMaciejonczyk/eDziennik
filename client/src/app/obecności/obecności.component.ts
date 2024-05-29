import { Component, Injector } from '@angular/core';
import { ObecnosciGenerated } from './obecności-generated.component';

@Component({
  selector: 'page-obecności',
  templateUrl: './obecności.component.html'
})
export class ObecnosciComponent extends ObecnosciGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
