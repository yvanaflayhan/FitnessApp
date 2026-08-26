import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Trainer } from "../_models/trainer";
import { TrainerRequest } from "../_models/trainer-request";
import { Gym } from "../_models/gym";
@Injectable({
    providedIn: 'root'
})
export class TrainerService {
    private baseUrl = 'https://localhost:5001/api/trainer';

    constructor(private http: HttpClient) { }

    createTrainerRequest(request: TrainerRequest) {
        return this.http.post<TrainerRequest>(
            this.baseUrl + '/request',
            request
        );
    }

    getTrainers() {
        return this.http.get<Trainer[]>(
            this.baseUrl
        );
    }

    submitTrainerRequest(request: TrainerRequest) {
        return this.http.post(`${this.baseUrl}/request`, request);
    }

    getGyms() {
        return this.http.get<Gym[]>(this.baseUrl);
    }

    getTrainerRequests() {
        return this.http.get<TrainerRequest[]>(
            'https://localhost:5001/api/admin/trainer-requests'
        );
    }

    approveTrainerRequest(id: number) {
        return this.http.put(
            `https://localhost:5001/api/admin/trainer-requests/${id}/approve`,
            {}
        );
    }

    rejectTrainerRequest(id: number) {
        return this.http.put(
            `https://localhost:5001/api/admin/trainer-requests/${id}/reject`,
            {}
        );
    }

}