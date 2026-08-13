import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { Account } from '../_services/account';
import { map } from 'rxjs';

export const adminGuard: CanActivateFn = () => {

  const account = inject(Account);
  const router = inject(Router);

  return account.currentUser$.pipe(
    map(user => {

      if (user?.role === 'Admin') {
        return true;
      }

      router.navigateByUrl('/not-found');
      return false;

    })
  );
};