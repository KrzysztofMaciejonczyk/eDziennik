import { Component, Injector } from '@angular/core';
import { AddNauczycielGenerated } from './add-nauczyciel-generated.component';

@Component({
  selector: 'page-add-nauczyciel',
  templateUrl: './add-nauczyciel.component.html'
})
export class AddNauczycielComponent extends AddNauczycielGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
