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

  gyms: Gym[] = [];
  selectedImage: File | null = null;
  selectedCv: File | null = null;

  trainerRequest: TrainerRequest = {
    age: undefined,
    gender: '',
    phone: '',
    height: undefined,
    weight: undefined,
    imageUrl: '',
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

  constructor(private trainerService: TrainerService, private changeDetector: ChangeDetectorRef, private toastr: ToastrService) { }

  ngOnInit() {
    this.loadTrainers();
    this.loadGyms();
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

    } else if (this.workplaceType === 'both') {

      this.trainerRequest.otherGymName = '';
      this.trainerRequest.worksIndependently = true;

    }

    const formData = new FormData();

    formData.append('Specialization', this.trainerRequest.specialization);
    formData.append(
      'WorksIndependently',
      String(this.trainerRequest.worksIndependently)
    );

    if (this.trainerRequest.age !== undefined)
      formData.append('Age', String(this.trainerRequest.age));

    if (this.trainerRequest.gender)
      formData.append('Gender', this.trainerRequest.gender);

    if (this.trainerRequest.phone)
      formData.append('Phone', this.trainerRequest.phone);

    if (this.trainerRequest.height !== undefined)
      formData.append('Height', String(this.trainerRequest.height));

    if (this.trainerRequest.weight !== undefined)
      formData.append('Weight', String(this.trainerRequest.weight));

    if (this.trainerRequest.yearsOfExperience !== undefined)
      formData.append(
        'YearsOfExperience',
        String(this.trainerRequest.yearsOfExperience)
      );

    if (this.trainerRequest.skills)
      formData.append('Skills', this.trainerRequest.skills);

    if (this.trainerRequest.gymId !== undefined)
      formData.append('GymId', String(this.trainerRequest.gymId));

    if (this.trainerRequest.otherGymName)
      formData.append('OtherGymName', this.trainerRequest.otherGymName);

    if (this.trainerRequest.description)
      formData.append('Description', this.trainerRequest.description);

    if (this.selectedImage)
      formData.append('image', this.selectedImage);

    if (this.selectedCv)
      formData.append('cv', this.selectedCv);


    // SEND REQUEST
    this.trainerService.submitTrainerRequest(formData).subscribe({
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

  loadGyms() {
    this.trainerService.getGyms().subscribe({//angular send get https://localhost:5001/api/gym
      next: gyms => {
        this.gyms = gyms; //we put response inside this.gyms 
        console.log('GYMS FROM API:', gyms);
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error loading gyms:', error);
      }
    });
  }
  onImageSelected(event: Event) {

    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {

      this.selectedImage = input.files[0];

      console.log('Selected image:', this.selectedImage);

    }

  }
  onCvSelected(event: Event) {

    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {

      this.selectedCv = input.files[0];

      console.log('Selected CV:', this.selectedCv);

    }

  }

}
