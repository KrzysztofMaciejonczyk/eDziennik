import { Component, Injector } from '@angular/core';
import { UzytkownicyGenerated } from './użytkownicy-generated.component';

@Component({
  selector: 'page-użytkownicy',
  templateUrl: './użytkownicy.component.html'
})
export class UzytkownicyComponent extends UzytkownicyGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
