import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Gym } from '../_models/gym';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GymService {

  baseUrl = environment.baseUrl + 'gym';

  constructor(private http: HttpClient) {}

  getGyms() {
    return this.http.get<Gym[]>(this.baseUrl);
  }

  getGym(id: number) {
    return this.http.get<Gym>(`${this.baseUrl}/${id}`);
  }
}