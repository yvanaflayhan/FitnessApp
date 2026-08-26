import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TrainerRequest } from '../../_models/trainer-request';
import { TrainerService } from '../../_services/trainer';

@Component({
  selector: 'app-admin-trainer-requests',
  imports: [],
  templateUrl: './admin-trainer-requests.html',
  styleUrl: './admin-trainer-requests.css',
})
export class AdminTrainerRequests implements OnInit {
  requests: TrainerRequest[] = [];
  loading = false;
  errorMessage = '';

  constructor(private trainerService: TrainerService, private changeDetector: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests(): void {
    this.loading = true;
    this.errorMessage = '';

    this.trainerService.getTrainerRequests().subscribe({
      next: requests => {
        this.requests = requests;
        this.loading = false;
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error loading trainer requests:', error);
        this.errorMessage = 'Unable to load trainer requests.';
        this.loading = false;
      },
      complete: () => {
        console.log('Request completed');
        this.loading = false;
      }
    });
  }
  approveRequest(id: number): void {
    this.trainerService.approveTrainerRequest(id).subscribe({
      next: () => {
        this.requests = this.requests.filter(r => r.id !== id);
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error approving trainer request:', error);
      }
    });
  }

  rejectRequest(id: number): void {
    this.trainerService.rejectTrainerRequest(id).subscribe({
      next: () => {
        this.requests = this.requests.filter(r => r.id !== id);
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error rejecting trainer request:', error);
      }
    });
  }
}
