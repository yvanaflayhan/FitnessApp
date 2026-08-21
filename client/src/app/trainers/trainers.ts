import { Component, OnInit } from '@angular/core';
import { TrainerService } from '../_services/trainer';
import { Trainer } from '../_models/trainer';
import { FormsModule } from '@angular/forms';
import { TrainerRequest } from '../_models/trainer-request';

@Component({
  selector: 'app-trainers',
  imports: [FormsModule],
  templateUrl: './trainers.html',
  styleUrl: './trainers.css',
})
export class Trainers implements OnInit {
  trainers: Trainer[] = []

  showTrainerForm = false;

  trainerRequest: TrainerRequest = {
    specialization: '',
    description: '',
    yearsOfExperience: undefined
  };

  requestSubmitted = false;
  requestError = '';

  constructor(private trainerService: TrainerService) { }

  ngOnInit(){
    this.loadTrainers();
  }

  loadTrainers(){
    this.trainerService.getTrainers().subscribe({
      next: trainers =>{
        this.trainers = trainers;
      },
      error: error => {
        console.error('Error loading trainers: ', error);
      }
    })

  }

  openTrainerForm(){
    this.showTrainerForm = true;
    this.requestSubmitted = false;
    this.requestError = '';
  }

  closeTrainerForm(){
    this.showTrainerForm = false;
  }

  submitTrainerRequest(){
    this.requestSubmitted = false;
    this.requestError = '';

    this.trainerService.submitTrainerRequest(this.trainerRequest).subscribe({
      next: () => {
        this.requestSubmitted = true;
        this.showTrainerForm = false;

        this.trainerRequest = {
          specialization: '',
          description: '',
          yearsOfExperience: undefined
        };
      },
      error: (error) => {
        this.requestError = error.error || 'Unable to submit your trainer request.'
      }
    });
  }
  
}
