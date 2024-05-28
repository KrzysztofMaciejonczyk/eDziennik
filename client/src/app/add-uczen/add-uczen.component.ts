import { Component, Injector } from '@angular/core';
import { AddUczenGenerated } from './add-uczen-generated.component';

@Component({
  selector: 'page-add-uczen',
  templateUrl: './add-uczen.component.html'
})
export class AddUczenComponent extends AddUczenGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
