import { Component, Injector } from '@angular/core';
import { EditKlasaGenerated } from './edit-klasa-generated.component';

@Component({
  selector: 'page-edit-klasa',
  templateUrl: './edit-klasa.component.html'
})
export class EditKlasaComponent extends EditKlasaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
