import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink} from '@angular/router';
import { TrainerService } from '../../_services/trainer';
import { Trainer } from '../../_models/trainer';

@Component({
  selector: 'app-trainer-profile',
  imports: [RouterLink],
  templateUrl: './trainer-profile.html',
  styleUrl: './trainer-profile.css',
})
export class TrainerProfile implements OnInit {
  
  trainer: Trainer | null = null;
  loading = true;
  error = '';

  constructor(private route: ActivatedRoute, private trainerService: TrainerService, private changeDetector: ChangeDetectorRef,){}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    if(!id){
      this.error = 'Invalid trainer.';
      this.loading = false;
      return;
    }
    this.loadTrainer(id);
  }

  loadTrainer(id: number) : void{
    this.trainerService.getTrainer(id).subscribe({
      next: trainer => {
        this.trainer = trainer;
        this.loading = false;
        this.changeDetector.detectChanges();

        console.log('TRAINER PROFILE:', trainer);
      },
      error: error => {
        console.error('Error loading trainer:', error);
        this.error = 'Unable to load trainer profile';
        this.loading = false;
      }
    });
  }
}
