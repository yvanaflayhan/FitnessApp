import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    baseUrl = environment.baseUrl + 'users';

    constructor(private http: HttpClient) { }

    updateCurrentUserLocation(
        latitude: number,
        longitude: number,
        location: string
    ) {

        return this.http.put(
            `${this.baseUrl}/current/location`,
            {
                latitude,
                longitude,
                location
            }
        );
    }

    updateCurrentUsername(username: string) {
        return this.http.put(
            `${this.baseUrl}/current/username`,
            {
                username
            }
        );
    }
}