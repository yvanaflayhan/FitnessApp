import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';
import { Gym } from '../_models/gym';

interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private baseUrl = 'https://localhost:5001/api/admin';

  constructor(private http: HttpClient) { }

  getUsers(pageNumber: number, pageSize: number) {
    return this.http.get<PagedResult<Member>>(
      this.baseUrl + '/users',
      {
        params: {
          pageNumber,
          pageSize
        }
      }
    );
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

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + '/users/' + id)
  }

  getGyms(pageNumber: number, pageSize: number) {
    return this.http.get<PagedResult<Gym>>(`${this.baseUrl}/gyms?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  getGym(id: number) {
    return this.http.get<Gym>(this.baseUrl + '/gyms/' + id);
  }

  addGym(gym: Gym) {
    return this.http.post<Gym>(this.baseUrl + '/gyms', gym);
  }

  updateGym(id: number, gym: Gym) {
    return this.http.put<Gym>(this.baseUrl + '/gyms/' + id, gym);
  }

  deleteGym(id: number) {
    return this.http.delete(this.baseUrl + '/gyms/' + id);
  }
}