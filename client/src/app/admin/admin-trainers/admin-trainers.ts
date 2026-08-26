import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TrainerService } from '../../_services/trainer';
import { Trainer } from '../../_models/trainer';
@Component({
  selector: 'app-admin-trainers',
  imports: [],
  templateUrl: './admin-trainers.html',
  styleUrl: './admin-trainers.css',
})
export class AdminTrainers implements OnInit {
  trainers: Trainer[]=[];
  loading = false;
  errorMessage = '';

  constructor(private trainerService: TrainerService, private changeDetector: ChangeDetectorRef){}

  ngOnInit(): void {
    this.loadTrainers();
  }

  loadTrainers(){
    this.loading = true;
    this.errorMessage = '';

    this.trainerService.getTrainers().subscribe({
      next: trainers =>{
        this.trainers = trainers;
        this.loading = false;
        this.changeDetector.detectChanges();
      },
      error: error =>{
        console.error('Error loading trainers:', error);
        this.errorMessage = 'Unable to load trainers.';
        this.loading = false;
      }
    })

  }
}
