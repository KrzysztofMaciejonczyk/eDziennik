import { Component, Injector } from '@angular/core';
import { EditUczenGenerated } from './edit-uczen-generated.component';

@Component({
  selector: 'page-edit-uczen',
  templateUrl: './edit-uczen.component.html'
})
export class EditUczenComponent extends EditUczenGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
