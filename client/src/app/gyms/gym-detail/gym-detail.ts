import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { GymService } from '../../_services/gym';
import { Gym } from '../../_models/gym';
import {ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-gym-detail',
  imports: [RouterLink],
  templateUrl: './gym-detail.html',
  styleUrl: './gym-detail.css',
})
export class GymDetail implements OnInit {

  gym?:Gym;

  constructor(private route: ActivatedRoute, private gymService: GymService, private changeDetector: ChangeDetectorRef) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.gymService.getGym(id).subscribe({
      next: gym => {
        this.gym = gym;
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error loading gym:', error);
      }
    });
  }
}
