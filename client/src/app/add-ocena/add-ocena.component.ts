import { Component, Injector } from '@angular/core';
import { AddOcenaGenerated } from './add-ocena-generated.component';

@Component({
  selector: 'page-add-ocena',
  templateUrl: './add-ocena.component.html'
})
export class AddOcenaComponent extends AddOcenaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
