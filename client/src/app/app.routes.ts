import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { OsobaComponent } from './osoba/osoba.component';
import { AddOsobaComponent } from './add-osoba/add-osoba.component';
import { EditOsobaComponent } from './edit-osoba/edit-osoba.component';
import { KlasaComponent } from './klasa/klasa.component';
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

import { SecurityService } from './security.service';
import { AuthGuard } from './auth.guard';
export const routes: Routes = [
  { path: '', redirectTo: '/osoba', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'osoba',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: OsobaComponent
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
        path: 'klasa',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: KlasaComponent
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
          roles: ['Authenticated'],
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
