import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';
import { Gym } from '../_models/gym';

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

  getGyms(){
    return this.http.get<Gym[]>(this.baseUrl + '/gyms');
  }

  getGym(id: number){
    return this.http.get<Gym>(this.baseUrl + '/gyms/' + id);
  }

  addGym(gym: Gym){
    return this.http.post<Gym>(this.baseUrl + '/gyms', gym);
  }

  UpdateGym(id: number, gym: Gym){
    return this.http.put<Gym>(this.baseUrl + '/gyms/' + id, gym);
  }

  DeleteGym(id: number){
    return this.http.delete<Gym>(this.baseUrl + '/gyms/' + id);
  }
}