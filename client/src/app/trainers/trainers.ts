import { Component, OnInit } from '@angular/core';
import { TrainerService } from '../_services/trainer';
import { Trainer } from '../_models/trainer';

@Component({
  selector: 'app-trainers',
  imports: [],
  templateUrl: './trainers.html',
  styleUrl: './trainers.css',
})
export class Trainers implements OnInit {
  trainers: Trainer[] = []

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
  
}
