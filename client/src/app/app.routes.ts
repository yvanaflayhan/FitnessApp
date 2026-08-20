import { Routes } from '@angular/router';
import { Home } from './home/home';
import { GymLists } from './gyms/gym-lists/gym-lists';
import { GymDetail } from './gyms/gym-detail/gym-detail';
import { Trainers } from './trainers/trainers';
import { Bookings } from './bookings/bookings';
import { Profile } from './profile/profile';
import { authGuard } from './_guards/auth-guard';
import { adminGuard } from './_guards/admin-guard';
import { TestError } from './errors/test-error/test-error';
import { NotFound } from './errors/not-found/not-found';
import { ServerError } from './errors/server-error/server-error';
import { Admin } from './admin/admin';
import { AdminUsers } from './admin/admin-users/admin-users';
import { AdminGyms } from './admin/admin-gyms/admin-gyms';
import { AdminTrainers } from './admin/admin-trainers/admin-trainers';
import { AdminBookings } from './admin/admin-bookings/admin-bookings';
import { AdminGymDetails } from './admin/admin-gym-details/admin-gym-details';

export const routes: Routes = [
    { path: '', component: Home, pathMatch: 'full' },
    //Normal User
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

        ]
    },
    //Admin
    {
        path: 'admin',
        canActivate: [authGuard, adminGuard],
        children: [
            { path: '', component: Admin },
            { path: 'users', component: AdminUsers },
            { path: 'gyms', component: AdminGyms },
            { path: 'trainers', component: AdminTrainers },
            { path: 'bookings', component: AdminBookings },
            { path: 'gyms/:id', component: AdminGymDetails }
        ]
    },
    //Error Pages 
    { path: 'errors', component: TestError },
    { path: 'not-found', component: NotFound },
    { path: 'server-error', component: ServerError },
    { path: '**', component: NotFound, pathMatch: 'full' },

];

