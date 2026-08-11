import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private baseUrl = 'https://localhost:5001/api/admin';

  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get<Member[]>(this.baseUrl + '/users');
  }

  getUser(id: number) {
    return this.http.get<Member>(this.baseUrl + '/users/' + id);
  }

  updateUser(id: number, member: Member) {
    return this.http.put<Member>(
      this.baseUrl + '/users/' + id,
      member
    );
  }
}