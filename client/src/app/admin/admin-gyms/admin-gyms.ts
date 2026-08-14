import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin';
import { Gym } from '../../_models/gym';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-gyms',
  imports: [FormsModule],
  templateUrl: './admin-gyms.html',
  styleUrl: './admin-gyms.css',
})
export class AdminGyms implements OnInit {
  gyms: Gym[] = [];

  showForm = false;
  showEditForm = false;
  editingGym: Gym | null = null;
  isSaving = false;

  newGym: Gym = {
    id: 0,
    name: '',
    address: '',
    latitude: 0,
    longitude: 0
  };

  constructor(private adminService: AdminService, private changeDetector: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.loadGyms();
  }

  loadGyms() {
    this.adminService.getGyms().subscribe({
      next: gyms => { this.gyms = gyms },
      error: error => {
        console.error('Error loading gyms:', error);
      }
    });
  }

  showAddForm() {
    this.showForm = true;
  }

  addGym() {
    if (this.isSaving) return;
    this.isSaving = true;
    this.adminService.addGym(this.newGym).subscribe({
      next: gym => {
        this.gyms.push(gym);
        this.showForm = false;
        this.newGym = {
          id: 0,
          name: '',
          address: '',
          latitude: 0,
          longitude: 0
        };
        this.isSaving = false;

        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error adding gym:', error);
        this.isSaving = false;
      }
    });
  }

  cancelAdd() {
    this.showForm = false;
  }

  editGym(gym: Gym) {
    this.editingGym = { ...gym };
    this.showEditForm = true;
  }

  saveEdit() {
    if (!this.editingGym) return;

    this.adminService.updateGym(this.editingGym.id, this.editingGym).subscribe({
      next: updatedGym => {
        const index = this.gyms.findIndex(gym => gym.id === updatedGym.id);
        if (index !== -1) this.gyms[index] = updatedGym;
        this.showEditForm = false;
        this.editingGym = null;
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error updating gym:', error);
      }
    });
  }

  cancelEdit() {
    this.showEditForm = false;
    this.editingGym = null;
  }

  deleteGym(id: number) {
    if (!confirm('Are you sure you want to delete this gym?')) return;

    this.adminService.deleteGym(id).subscribe({
      next: () => {
        this.gyms = this.gyms.filter(gym => gym.id !== id);
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error deleting gym:', error);
      }
    });
  }
}