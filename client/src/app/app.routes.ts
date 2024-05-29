import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { UzytkownicyComponent } from './użytkownicy/użytkownicy.component';
import { AddOsobaComponent } from './add-osoba/add-osoba.component';
import { EditOsobaComponent } from './edit-osoba/edit-osoba.component';
import { KlasyComponent } from './klasy/klasy.component';
import { AddKlasaComponent } from './add-klasa/add-klasa.component';
import { EditKlasaComponent } from './edit-klasa/edit-klasa.component';
import { NauczycielComponent } from './nauczyciel/nauczyciel.component';
import { AddNauczycielComponent } from './add-nauczyciel/add-nauczyciel.component';
import { EditNauczycielComponent } from './edit-nauczyciel/edit-nauczyciel.component';
import { ApplicationUsersComponent } from './application-users/application-users.component';
import { AddApplicationRoleComponent } from './add-application-role/add-application-role.component';
import { ApplicationRolesComponent } from './application-roles/application-roles.component';
import { RegisterApplicationUserComponent } from './register-application-user/register-application-user.component';
import { EditApplicationUserComponent } from './edit-application-user/edit-application-user.component';
import { AddApplicationUserComponent } from './add-application-user/add-application-user.component';
import { ProfileComponent } from './profile/profile.component';
import { LoginComponent } from './login/login.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { RodzicComponent } from './rodzic/rodzic.component';
import { AddRodzicComponent } from './add-rodzic/add-rodzic.component';
import { EditRodzicComponent } from './edit-rodzic/edit-rodzic.component';
import { UczenComponent } from './uczen/uczen.component';
import { AddUczenComponent } from './add-uczen/add-uczen.component';
import { EditUczenComponent } from './edit-uczen/edit-uczen.component';
import { OcenaComponent } from './ocena/ocena.component';
import { AddOcenaComponent } from './add-ocena/add-ocena.component';
import { EditOcenaComponent } from './edit-ocena/edit-ocena.component';
import { ObecnoscComponent } from './obecnosc/obecnosc.component';
import { AddObecnoscComponent } from './add-obecnosc/add-obecnosc.component';
import { EditObecnoscComponent } from './edit-obecnosc/edit-obecnosc.component';
import { ObecnosciComponent } from './obecności/obecności.component';

import { SecurityService } from './security.service';
import { AuthGuard } from './auth.guard';
export const routes: Routes = [
  { path: '', redirectTo: '/użytkownicy', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'użytkownicy',
        canActivate: [AuthGuard],
        data: {
          roles: ['Admin'],
        },
        component: UzytkownicyComponent
      },
      {
        path: 'add-osoba',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddOsobaComponent
      },
      {
        path: 'edit-osoba/:osoba_id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditOsobaComponent
      },
      {
        path: 'klasy',
        canActivate: [AuthGuard],
        data: {
          roles: ['Admin'],
        },
        component: KlasyComponent
      },
      {
        path: 'add-klasa',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddKlasaComponent
      },
      {
        path: 'edit-klasa/:klasa_id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditKlasaComponent
      },
      {
        path: 'nauczyciel',
        canActivate: [AuthGuard],
        data: {
          roles: ['Admin'],
        },
        component: NauczycielComponent
      },
      {
        path: 'add-nauczyciel',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddNauczycielComponent
      },
      {
        path: 'edit-nauczyciel/:nauczyciel_id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditNauczycielComponent
      },
      {
        path: 'application-users',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ApplicationUsersComponent
      },
      {
        path: 'add-application-role',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddApplicationRoleComponent
      },
      {
        path: 'application-roles',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ApplicationRolesComponent
      },
      {
        path: 'register-application-user',
        data: {
          roles: ['Everybody'],
        },
        component: RegisterApplicationUserComponent
      },
      {
        path: 'edit-application-user/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditApplicationUserComponent
      },
      {
        path: 'add-application-user',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddApplicationUserComponent
      },
      {
        path: 'profile',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ProfileComponent
      },
      {
        path: 'unauthorized',
        data: {
          roles: ['Everybody'],
        },
        component: UnauthorizedComponent
      },
      {
        path: 'rodzic',
        canActivate: [AuthGuard],
        data: {
          roles: ['Admin'],
        },
        component: RodzicComponent
      },
      {
        path: 'add-rodzic',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated', 'Rodzic'],
        },
        component: AddRodzicComponent
      },
      {
        path: 'edit-rodzic/:rodzic_id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated', 'Rodzic'],
        },
        component: EditRodzicComponent
      },
      {
        path: 'uczen',
        canActivate: [AuthGuard],
        data: {
          roles: ['Admin'],
        },
        component: UczenComponent
      },
      {
        path: 'add-uczen',
        canActivate: [AuthGuard],
        data: {
          roles: ['Admin'],
        },
        component: AddUczenComponent
      },
      {
        path: 'edit-uczen/:uczen_id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Admin'],
        },
        component: EditUczenComponent
      },
      {
        path: 'ocena',
        canActivate: [AuthGuard],
        data: {
          roles: ['Nauczyciel'],
        },
        component: OcenaComponent
      },
      {
        path: 'add-ocena',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated', 'Nauczyciel'],
        },
        component: AddOcenaComponent
      },
      {
        path: 'edit-ocena/:ocena_id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated', 'Nauczyciel'],
        },
        component: EditOcenaComponent
      },
      {
        path: 'obecnosc',
        canActivate: [AuthGuard],
        data: {
          roles: ['Nauczyciel'],
        },
        component: ObecnoscComponent
      },
      {
        path: 'add-obecnosc',
        canActivate: [AuthGuard],
        data: {
          roles: ['Nauczyciel'],
        },
        component: AddObecnoscComponent
      },
      {
        path: 'edit-obecnosc/:ocena_id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Nauczyciel'],
        },
        component: EditObecnoscComponent
      },
      {
        path: 'obecności',
        canActivate: [AuthGuard],
        data: {
          roles: ['Uczeń', 'Rodzic'],
        },
        component: ObecnosciComponent
      },
    ]
  },
  {
    path: '',
    component: LoginLayoutComponent,
    children: [
      {
        path: 'login',
        data: {
          roles: ['Everybody'],
        },
        component: LoginComponent
      },
    ]
  },
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
