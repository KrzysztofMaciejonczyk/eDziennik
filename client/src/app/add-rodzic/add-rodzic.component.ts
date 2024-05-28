import { Component, Injector } from '@angular/core';
import { AddRodzicGenerated } from './add-rodzic-generated.component';

@Component({
  selector: 'page-add-rodzic',
  templateUrl: './add-rodzic.component.html'
})
export class AddRodzicComponent extends AddRodzicGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
