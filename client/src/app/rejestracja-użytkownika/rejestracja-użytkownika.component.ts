import { Component, Injector } from '@angular/core';
import { RejestracjaUzytkownikaGenerated } from './rejestracja-użytkownika-generated.component';

@Component({
  selector: 'page-rejestracja-użytkownika',
  templateUrl: './rejestracja-użytkownika.component.html'
})
export class RejestracjaUzytkownikaComponent extends RejestracjaUzytkownikaGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
