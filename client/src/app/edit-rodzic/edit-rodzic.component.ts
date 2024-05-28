import { Component, Injector } from '@angular/core';
import { EditRodzicGenerated } from './edit-rodzic-generated.component';

@Component({
  selector: 'page-edit-rodzic',
  templateUrl: './edit-rodzic.component.html'
})
export class EditRodzicComponent extends EditRodzicGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
