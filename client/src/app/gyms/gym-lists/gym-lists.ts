import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { GymService } from '../../_services/gym';
import { Gym } from '../../_models/gym';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-gym-lists',
  imports: [RouterLink, FormsModule],
  templateUrl: './gym-lists.html',
  styleUrl: './gym-lists.css',
})
export class GymLists implements OnInit {

  gyms: Gym[] = [];
  filteredGyms: Gym[] = [];
  searchTerm = '';

  constructor(private gymService: GymService, private changeDetector: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.loadGyms();
  }

  loadGyms(): void {
    this.gymService.getGyms().subscribe({
      next: gyms => {
        this.gyms = gyms;
        this.filteredGyms = gyms;
        this.changeDetector.detectChanges();

        console.log('GYMS:', gyms);
      },
      error: error => {
        console.error('Error loading gyms:', error);
      }
    });
  }

  sortByNearest(): void {
    // We will implement the real sorting later
  }

  filterGyms(): void {
  const term = this.searchTerm.trim().toLowerCase();

  if (!term) {
    this.filteredGyms = this.gyms;
    return;
  }

  this.filteredGyms = this.gyms.filter(gym =>
    gym.name.toLowerCase().includes(term) ||
    gym.address.toLowerCase().includes(term)
  );
}
}
