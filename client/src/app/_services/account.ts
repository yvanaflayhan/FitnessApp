import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class Account {
  baseUrl = environment.baseUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          sessionStorage.setItem('user', JSON.stringify(user));
          console.log('Saved:', sessionStorage.getItem('user'));
          this.currentUserSource.next(user);
        }
        return user;
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if (user) {
          sessionStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )

  }

  setCurentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    sessionStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  validateToken() {
    return this.http.get<User>(this.baseUrl + 'account/validate');
  }
  getCurrentUser() {
    return this.http.get<User>(this.baseUrl + 'users/current');
  }
}
