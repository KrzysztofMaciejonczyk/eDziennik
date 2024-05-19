import { Component, Injector } from '@angular/core';
import { EditNauczycielGenerated } from './edit-nauczyciel-generated.component';

@Component({
  selector: 'page-edit-nauczyciel',
  templateUrl: './edit-nauczyciel.component.html'
})
export class EditNauczycielComponent extends EditNauczycielGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
