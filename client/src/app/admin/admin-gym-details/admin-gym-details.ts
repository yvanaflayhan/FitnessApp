import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin';
import { ActivatedRoute } from '@angular/router';
import { Gym } from '../../_models/gym';

@Component({
  selector: 'app-admin-gym-details',
  imports: [],
  templateUrl: './admin-gym-details.html',
  styleUrl: './admin-gym-details.css',
})
export class AdminGymDetails implements OnInit {
  gym: Gym | null = null;

  constructor(private route: ActivatedRoute, private adminService: AdminService, private changeDetector: ChangeDetectorRef ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.loadGym(id);
  }

  loadGym(id: number): void {
    this.adminService.getGym(id).subscribe({
      next: gym => {
        console.log('Gym returned from API:', gym);

        this.gym = gym;

        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error loading gym:', error);
      }
    })
  }
}
