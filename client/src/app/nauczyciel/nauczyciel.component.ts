import { Component, Injector } from '@angular/core';
import { NauczycielGenerated } from './nauczyciel-generated.component';

@Component({
  selector: 'page-nauczyciel',
  templateUrl: './nauczyciel.component.html'
})
export class NauczycielComponent extends NauczycielGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
