import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Trainer } from "../_models/trainer";
import { TrainerRequest } from "../_models/trainer-request";
import { Gym } from "../_models/gym";
import { environment } from "../../environments/environment";
@Injectable({
    providedIn: 'root'
})
export class TrainerService {
    baseUrl = environment.baseUrl + 'trainer';

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

    getTrainer(id: number) {
        return this.http.get<Trainer>(
            `${this.baseUrl}/${id}`
        );
    }

    submitTrainerRequest(formData: FormData) {
        return this.http.post(`${this.baseUrl}/request`, formData);
    }

    getGyms() {
        return this.http.get<Gym[]>(environment.baseUrl + 'gym');
    }

    getTrainerRequests() {
        return this.http.get<TrainerRequest[]>(environment.baseUrl + 'admin/trainer-requests');
    }

    approveTrainerRequest(id: number) {
        return this.http.put(
            `${environment.baseUrl}admin/trainer-requests/${id}/approve`,
            {}
        );
    }

    rejectTrainerRequest(id: number) {
        return this.http.put(
            `${environment.baseUrl}admin/trainer-requests/${id}/reject`,
            {}
        );
    }

}