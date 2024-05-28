import { Component, Injector } from '@angular/core';
import { UczenGenerated } from './uczen-generated.component';

@Component({
  selector: 'page-uczen',
  templateUrl: './uczen.component.html'
})
export class UczenComponent extends UczenGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
