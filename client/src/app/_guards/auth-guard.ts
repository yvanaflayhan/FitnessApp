import { CanActivateFn } from '@angular/router';
import { Account } from '../_services/account';
import { ToastrService } from 'ngx-toastr';
import { inject } from '@angular/core';
import { map, of, switchMap, catchError } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const account = inject(Account);
  const toastr = inject(ToastrService);
  return account.currentUser$.pipe(
    switchMap(user => {

      // User is already loaded
      if (user) {
        return of(true);
      }

      // No user currently loaded.
      // Ask the API to validate the saved token.
      const userJson = sessionStorage.getItem('user');

      if (!userJson) {
        toastr.error('You shall not pass!');
        return of(false);
      }

      return account.getCurrentUser().pipe(

        map(currentUser => {

          account.setCurentUser(currentUser);

          sessionStorage.setItem(
            'user',
            JSON.stringify(currentUser)
          );

          return true;
        }),

        catchError(() => {

          account.logout();

          toastr.error('Your session has expired. Please login again.');

          return of(false);
        })

      );

    })
  )
};
