import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserSessionComponent } from './user-session/user-session.component';

export const routes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'user-session', component: UserSessionComponent },
];
