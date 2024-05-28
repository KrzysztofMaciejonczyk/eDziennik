import { Component, Injector } from '@angular/core';
import { EditOcenaGenerated } from './edit-ocena-generated.component';

@Component({
  selector: 'page-edit-ocena',
  templateUrl: './edit-ocena.component.html'
})
export class EditOcenaComponent extends EditOcenaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
