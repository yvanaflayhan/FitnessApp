import { CanActivateFn } from '@angular/router';
import { Account } from '../_services/account';
import { ToastrService } from 'ngx-toastr';
import { inject } from '@angular/core';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const account = inject(Account);
  const toastr = inject(ToastrService);
  return account.currentUser$.pipe(
    map(user => {
      if(user) return true;
      else{
        toastr.error('You shall not pass!');
        return false;
        
      }
    })
  )
};
