import { Routes } from '@angular/router';
import { Home } from './home/home';
import { GymLists } from './gyms/gym-lists/gym-lists';
import { GymDetail } from './gyms/gym-detail/gym-detail';
import { Trainers } from './trainers/trainers';
import { Bookings } from './bookings/bookings';
import { Profile } from './profile/profile';
import { authGuard } from './_guards/auth-guard';
import { TestError } from './errors/test-error/test-error';
import { NotFound } from './errors/not-found/not-found';
import { ServerError } from './errors/server-error/server-error';
import { Admin } from './admin/admin';

export const routes: Routes = [
    { path: '', component: Home },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children: [
            { path: 'gyms', component: GymLists },
            { path: 'gyms/:id', component: GymDetail },
            { path: 'trainers', component: Trainers },
            { path: 'bookings', component: Bookings },
            { path: 'profile', component: Profile },
            { path: 'admin', component: Admin },

        ]
    },
    {path: 'errors', component: TestError},
    {path: 'not-found', component: NotFound},
    {path: 'server-error', component: ServerError},
    { path: '**', component: NotFound, pathMatch: 'full' },

];

