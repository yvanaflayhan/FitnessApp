import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TrainerService } from '../_services/trainer';
import { Trainer } from '../_models/trainer';
import { FormsModule } from '@angular/forms';
import { TrainerRequest } from '../_models/trainer-request';
import { Gym } from '../_models/gym';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-trainers',
  imports: [FormsModule],
  templateUrl: './trainers.html',
  styleUrl: './trainers.css',
})
export class Trainers implements OnInit {
  trainers: Trainer[] = []
  searchTerm = '';

  get filteredTrainers(): Trainer[] {
    const term = this.searchTerm.trim().toLowerCase();
    if (!term) return this.trainers;
    return this.trainers.filter(t => t.specialization?.toLowerCase().includes(term));
  }

  showTrainerForm = false;

  workplaceType = '';

  gyms: any[] = [];

  trainerRequest: TrainerRequest = {
    age: undefined,
    gender: '',
    phone: '',
    height: undefined,
    weight: undefined,
    imageUrl:'',
    specialization: '',
    description: '',
    yearsOfExperience: undefined as number | undefined,
    skills: '',
    gymId: undefined as number | undefined,
    otherGymName: '',
    worksIndependently: false,
    cvUrl: ''
  };

  requestSubmitted = false;
  requestError = '';

  constructor(private trainerService: TrainerService, private changeDetector: ChangeDetectorRef, private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.loadTrainers();
  }

  loadTrainers() {
    this.trainerService.getTrainers().subscribe({
      next: trainers => {
        this.trainers = trainers;
        this.changeDetector.detectChanges();
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

          this.toastr.success('Your trainer request has been submitted successfully.',
            'Request Submitted'
          )

          this.requestSubmitted = true;
          this.showTrainerForm = false;

          this.trainerRequest = {
            age: undefined,
            gender: '',
            phone: '',
            height: undefined,
            weight: undefined,
            imageUrl: '',
            specialization: '',
            description: '',
            yearsOfExperience: undefined,
            skills: '',
            gymId: undefined,
            otherGymName: '',
            worksIndependently: false,
            cvUrl: ''
          };

          this.workplaceType = '';
          this.changeDetector.detectChanges();

        },

        error: error => {
          console.error('Error submitting trainer request:', error);

          const message =
            typeof error.error === 'string'
              ? error.error
              : error.error?.message || 'Unable to submit trainer request.';

          this.toastr.error(message, 'Trainer Request');
        }
      });
  }

}
