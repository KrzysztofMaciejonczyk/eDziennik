import { Component, Injector } from '@angular/core';
import { EditObecnoscGenerated } from './edit-obecnosc-generated.component';

@Component({
  selector: 'page-edit-obecnosc',
  templateUrl: './edit-obecnosc.component.html'
})
export class EditObecnoscComponent extends EditObecnoscGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
