import { Component, OnInit } from '@angular/core';
import { TrainerService } from '../_services/trainer';
import { Trainer } from '../_models/trainer';
import { FormsModule } from '@angular/forms';
import { TrainerRequest } from '../_models/trainer-request';
import { Gym } from '../_models/gym';

@Component({
  selector: 'app-trainers',
  imports: [FormsModule],
  templateUrl: './trainers.html',
  styleUrl: './trainers.css',
})
export class Trainers implements OnInit {
  trainers: Trainer[] = []

  showTrainerForm = false;

  workplaceType = '';

  gyms: any[] = [];

  trainerRequest: TrainerRequest = {
    specialization: '',
    description: '',
    yearsOfExperience: undefined as number | undefined,
    gymId: undefined as number | undefined,
    otherGymName: '',
    worksIndependently: false
  };

  requestSubmitted = false;
  requestError = '';

  constructor(private trainerService: TrainerService) { }

  ngOnInit() {
    this.loadTrainers();
  }

  loadTrainers() {
    this.trainerService.getTrainers().subscribe({
      next: trainers => {
        this.trainers = trainers;
      },
      error: error => {
        console.error('Error loading trainers: ', error);
      }
    })

  }

  openTrainerForm() {
    this.showTrainerForm = true;
    this.requestSubmitted = false;
    this.requestError = '';
  }

  closeTrainerForm() {
    this.showTrainerForm = false;
    this.requestError = '';
  }

  submitTrainerRequest() {

    this.requestError = '';

    if (this.workplaceType === 'fitnessGym') {

      this.trainerRequest.otherGymName = '';
      this.trainerRequest.worksIndependently = false;

    } else if (this.workplaceType === 'otherGym') {

      this.trainerRequest.gymId = undefined;
      this.trainerRequest.worksIndependently = false;

    } else if (this.workplaceType === 'independent') {

      this.trainerRequest.gymId = undefined;
      this.trainerRequest.otherGymName = '';
      this.trainerRequest.worksIndependently = true;

    }

    this.trainerService.submitTrainerRequest(this.trainerRequest)
      .subscribe({
        next: () => {

          this.requestSubmitted = true;
          this.showTrainerForm = false;

          this.trainerRequest = {
            specialization: '',
            description: '',
            yearsOfExperience: undefined,
            gymId: undefined,
            otherGymName: '',
            worksIndependently: false
          };

          this.workplaceType = '';
        },

        error: error => {
          console.error('Error submitting trainer request:', error);

          this.requestError =
            error.error || 'Unable to submit trainer request.';
        }
      });
  }

}
