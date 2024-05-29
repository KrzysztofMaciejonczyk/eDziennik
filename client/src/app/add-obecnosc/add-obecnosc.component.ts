import { Component, Injector } from '@angular/core';
import { AddObecnoscGenerated } from './add-obecnosc-generated.component';

@Component({
  selector: 'page-add-obecnosc',
  templateUrl: './add-obecnosc.component.html'
})
export class AddObecnoscComponent extends AddObecnoscGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
